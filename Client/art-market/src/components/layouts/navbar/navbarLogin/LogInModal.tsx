import React, { useState } from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import Modal from '@mui/material/Modal';
import TextField from '@mui/material/TextField';
import { Formik, Form } from 'formik';
import * as Yup from 'yup';
import VisibilityOff from "@mui/icons-material/VisibilityOff";
import { IconButton, InputAdornment } from '@mui/material';
import { Visibility } from '@mui/icons-material';
import CloseIcon from '@mui/icons-material/Close';
import { useAuth } from '../../../../contexts/AuthContext';
import axios from '../../../../configurations/axios/axiosConfig'; 
import ForgetPasswordModal from './ForgetPasswordModal';
import '../../styles/LogInModalStyle.css'
import RegisterModalForm from './RegisterModalForm';
import toast from 'react-hot-toast';

export interface LogInModel{
  username: string;
  password: string;
}

const validationSchema = Yup.object({
  username: Yup.string().required('Username is required'),
  password: Yup.string().required('Password is required'),
});

const style = {
  position: 'absolute' as 'absolute',
  top: '50%',
  left: '50%',
  transform: 'translate(-50%, -50%)',
  width: 350,
  bgcolor: 'white',
  boxShadow: 24,
  p: 3,
};


const fieldStyles = {
    backgroundColor: 'transparent',
    boxShadow: 'none',

}

const buttonStyle = {
    backgroundColor: 'white',
    color: 'black',
    outline: 'none',
    height: '10px',
}

const LogInModal: React.FC = () => {
  const { login , setUser } = useAuth();
  const [open, setOpen] = useState(false);
  const [showPassword, setShowPassword] = useState(false);
  const [forgetPasswordModal, setForgetPasswordModal] = useState(false);
  const [registerModal, setRegisterModal] = useState(false);


  const handleforgetPasswordModalOpen = () => {
    handleClose();
    setForgetPasswordModal(true);
  }
  const handleforgetPasswordModalClose = () => setForgetPasswordModal(false);  

  const handleRegisterModalOpen = () => {
    handleClose();
    setRegisterModal(true);
  }
  const handleRegisterModalClose = () => setRegisterModal(false);  

  const handleClickShowPassword = () => setShowPassword(!showPassword);
  const handleMouseDownPassword = () => setShowPassword(!showPassword);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  const initialValues: LogInModel = {
    username: '',
    password: '',
  };

  const handleLogin = async (values: LogInModel) => {
    toast.promise(
      axios.post('/Account/login', values)
      .then(async response => {
        const token = response.data;
        login(token);
        handleClose();
        try {
          const userResponse = await axios.get('/Account/user'); 
          const user = userResponse.data;
          setUser(user);
          localStorage.setItem('user', JSON.stringify(user)); 
      } catch (error) {
          console.error('Failed to fetch user details', error);
      }
      }),
      {
        loading: 'Logging in...',
        success: <b>Logged in successfully!</b>,
        error: <b>Failed to log in.</b>,
      }
    ).catch(error => {
      console.error('Failed to login', error);
    });
  }

  return (
    <div>
      <Button style={{ textTransform: 'capitalize', fontSize: '17px' , outline: 'none', color: 'black' }} onClick={handleOpen}>
        SignIn/Register
      </Button>
      <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="login-modal"
        aria-describedby="modal-for-login"
      >
        <Box sx={style}>
            <IconButton
            aria-label="close"
            onClick={handleClose}
            sx={{
                position: 'absolute',
                right: 8,
                top: 8,
                color: (theme) => theme.palette.grey[500],
                outline: 'none',
            }}
            >
                <CloseIcon />
            </IconButton>
          <Typography id="modal-modal-title" variant="h5" style={{color: 'black' , marginBottom: '16px'}}>
             Log in to collect art by the world’s leading artists
          </Typography>
          <Formik
            initialValues={initialValues}
            validationSchema={validationSchema}
            onSubmit= {handleLogin}
          >
            {({ isSubmitting, handleChange, handleBlur , touched, errors}) => (
              <Form>
                <Box sx={{ '& .MuiTextField-root': {  marginBottom: '16px' } }}>
                  <div>
                    <TextField
                      id="username"
                      label="Username"
                      name="username"
                      variant="outlined"
                      onChange={handleChange}
                      onBlur={handleBlur}
                      fullWidth
                      style={fieldStyles}
                      error={touched.username && Boolean(errors.username)}
                      helperText = {touched.username && errors.username}
                    />
                   
                  </div>
                  <div>
                    <TextField
                      id="password"
                      label="Password"
                      name="password"
                      variant="outlined"
                      type={showPassword ? "text" : "password"}
                      onChange={handleChange}
                      onBlur={handleBlur}
                      fullWidth                      
                      error={touched.password && Boolean(errors.password)}
                      helperText = {touched.password && errors.password}
                      InputProps={{ 
                        endAdornment: (
                          <InputAdornment position="end">
                            <IconButton
                              aria-label="toggle password visibility"
                              onClick={handleClickShowPassword}
                              onMouseDown={handleMouseDownPassword}
                            >
                              {showPassword ? <Visibility /> : <VisibilityOff />}
                            </IconButton>
                          </InputAdornment>
                        )
                      }}
                    />
                  </div>
                  <div style={{alignItems: 'center', display: 'flex', justifyContent: 'flex-end' ,marginRight: '5px'}}>
                    <Button style={buttonStyle} onClick={handleforgetPasswordModalOpen}  variant="text">Forgot Password?</Button>     
                  </div>
                  <br />
                  <div style={{alignItems: 'center', display: 'flex', justifyContent: 'flex-end' ,marginRight: '5px'}}>
                    <Button style={buttonStyle} onClick={handleRegisterModalOpen}  variant="text">Register</Button>     
                  </div>
                  <Button type="submit" variant="contained" color="primary" disabled={isSubmitting}>
                    Submit
                  </Button>
                </Box>
              </Form>
            )}
          </Formik>
        </Box>
      </Modal>
      <ForgetPasswordModal isOpened = {forgetPasswordModal} onClose={handleforgetPasswordModalClose}/>
      <RegisterModalForm isOpened = {registerModal} onClose={handleRegisterModalClose}/>

    </div>
  );
};

export default LogInModal;
