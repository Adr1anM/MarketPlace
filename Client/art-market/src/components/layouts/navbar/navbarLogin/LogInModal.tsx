import React, { useEffect, useState } from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import Modal from '@mui/material/Modal';
import TextField from '@mui/material/TextField';
import { Formik, Form } from 'formik';
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
import LogInModel from './LogInModel';
import { buttonStyle, fieldStyles, style, validationSchema } from './loginModalStyle';
import useShoppingCart from '../../../../zsm/stores/useShoppingCart';
import { Link } from 'react-router-dom';

interface LogInModalProps{
  isOpened: boolean;
  onClose: () => void;
  title: string;
}

const LogInModal: React.FC<LogInModalProps> = ({isOpened,onClose,title}) => {
  const { login , setUser } = useAuth();
  const cartStore = useShoppingCart();
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
  useEffect(() => {
    setOpen(isOpened);
  }, [isOpened]);
  
  
  const handleClose = () =>{
     setOpen(false);
     onClose();
  }

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
          await cartStore.fetchCart(user.id);
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
             {title}
          </Typography>
          <Formik
            initialValues={initialValues}
            validationSchema={validationSchema}
            onSubmit= {handleLogin}
          >
            {({ isSubmitting, handleChange, handleBlur , touched, errors}) => (
              <Form>
                <Box sx={{ '& .MuiTextField-root': {  marginBottom: '16px' } }}>
                  <Box>
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
                   
                  </Box>
                  <Box>
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
                  </Box>
                  <Box style={{alignItems: 'center', display: 'flex', justifyContent: 'flex-end' ,marginRight: '5px'}}>
                    <Button style={buttonStyle} onClick={handleforgetPasswordModalOpen}  variant="text">Forgot Password?</Button>     
                  </Box>
                  <br />
                  <Box style={{alignItems: 'center', display: 'flex', justifyContent: 'flex-end' ,marginRight: '5px'}}>
                    <Link to="/register" style={{ textDecoration: 'none' }}>
                      <Button style={buttonStyle} onClick={() => onClose()} variant="text">Register</Button>
                    </Link>         
                  </Box>
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
