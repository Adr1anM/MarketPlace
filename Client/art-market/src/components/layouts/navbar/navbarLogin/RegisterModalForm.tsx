import React, { useEffect, useState } from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import Modal from '@mui/material/Modal';
import TextField from '@mui/material/TextField';
import { Formik, Form, FormikHelpers } from 'formik';
import * as Yup from 'yup';
import VisibilityOff from "@mui/icons-material/VisibilityOff";
import { Checkbox, FormControlLabel, IconButton, InputAdornment, Stack } from '@mui/material';
import { Visibility } from '@mui/icons-material';
import CloseIcon from '@mui/icons-material/Close';
import { useAuth } from '../../../../contexts/AuthContext';
import axios from '../../../../configurations/axios/axiosConfig'; 
import '../../styles/LogInModalStyle.css'
import InputFileUpload from '../../../pages/profile/InputFileUpload';
import { Author } from '../../../../types/types';

interface RegisterData {
  firstName: string;
  lastName: string;
  userName: string;
  email: string;
  password: string;
  role: string;
}

interface RegisterCommand {
  registerData: RegisterData;
}


const validationSchema = Yup.object({
    firstName: Yup.string()
          .min(2, 'First Name must be at least 5 characters long')
          .required('First Name is required'),
    lastName: Yup.string()
          .min(2, 'Last Name must be at least 5 characters long')
          .required('Last Name is required'),
    userName: Yup.string()
          .min(5, 'Username must be at least 5 characters long')
          .max(50,'Username is too long')
          .matches(/[A-Z]/, 'Username must contain at least one uppercase letter')
          .required('Username is required'),
    email: Yup.string().email('Invalid email').required('Email is required'),
    password: Yup.string() 
          .min(6, 'Password must be at least 6 characters long')
          .required('Password is required')
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

interface RegisterModalProps {
    isOpened: boolean;
    onClose: () => void;
}

const RegisterModalForm: React.FC<RegisterModalProps> = ({isOpened,onClose}) => {
  const { login } = useAuth();
  const [open, setOpen] = useState(false);
  const [showPassword, setShowPassword] = useState(false);
  const [registerAsAuthor, setRegisterAsAuthor] = useState(false);
  const [authorData, setAuthorData] = useState<Author | null>();

  const handleClickShowPassword = () => setShowPassword(!showPassword);
  const handleMouseDownPassword = () => setShowPassword(!showPassword);

  useEffect(() => {
    setOpen(isOpened);
  }, [isOpened]);
  
  
  const handleClose = () =>{
    setOpen(false);
    onClose();
  }

  

  const initialValues: RegisterData = {
      firstName: '',
      lastName: '',
      userName: '',
      email: '',
      password: '',
      role: 'User'
  };

  const handleRegister = async (values: RegisterData, { setErrors, setSubmitting, resetForm }: FormikHelpers<RegisterData>) => {

    const role = registerAsAuthor ? 'Author' : 'User';
    const registerCommand: RegisterCommand = {
      registerData: { ...values, role }
    };

    console.log("register", registerCommand);
    setSubmitting(true);
    try {
      const response = await axios.post('/Account/register', registerCommand);
      console.log(response);
      const token = response.data;
      login(token);
      resetForm();
      handleClose();
    } catch (error) {
      console.error('Failed to register', error);
      if (axios.isAxiosError(error) && error.response) {
        const serverError = error.response.data;
        if (typeof serverError === 'string') {
          alert(serverError);
        } else if (serverError.errors) {
          const formErrors: { [key: string]: string } = {};
          Object.keys(serverError.errors).forEach(key => {
            formErrors[`registerData.${key.toLowerCase()}`] = serverError.errors[key][0];
          });
          setErrors(formErrors);
        }
      } else {
        alert('An unexpected error occurred');
      }
    } finally {
      setSubmitting(false);
    }
  };

  const handleUpload = (file: File) => {
    try {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => {
        const base64String = reader.result as string;
        const updatedEntity = {
          ...authorData!,
          profileImage: base64String.replace(/^data:image\/[a-z]+;base64,/, "")
        };
        setAuthorData(updatedEntity);
      };
    } catch (error) {
      console.error('Error uploading image:', error);
    }
  };

  return (
    <div>
      <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="register-modal"
        aria-describedby="modal-for-register"
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
          <Typography id="modal-modal-title" variant='h4' style={{color: 'black' , marginBottom: '16px'}}>
            Register
          </Typography>


          {(registerAsAuthor && authorData?.profileImage) &&
            <Box
              component="img"
              sx={{
                height: 200,
                width: 200, 
                objectFit: 'cover'
              }}
              alt="The house from the offer."
              src={`data:image/jpeg;base64,${authorData?.profileImage?.toString()}`}
            />
          }
          {registerAsAuthor && !authorData?.profileImage &&
            <Box
              component="img"
              sx={{
                height: 200,
                width: 200,
                objectFit: 'cover'
              }}
              alt="The house from the offer."
              src={"https://t4.ftcdn.net/jpg/05/17/53/57/360_F_517535712_q7f9QC9X6TQxWi6xYZZbMmw5cnLMr279.jpg"}
            />
          }
         {registerAsAuthor &&
          <Box>
            <Stack sx={{ marginTop: '15px' }} direction="row" gap="10px">
              <Button>Delete</Button>
              <InputFileUpload onUpload={handleUpload} />
            </Stack>
          </Box>
        }
          <Formik
            initialValues={initialValues}
            validationSchema={validationSchema}
            onSubmit= {handleRegister}
          > 
            {({ isSubmitting, handleChange, handleBlur , touched, errors}) => (
              <Form>
                <Box sx={{ '& .MuiTextField-root': {  marginBottom: '16px' } }}>
                  <div>
                    <TextField
                      id="firstName"
                      label="First Name"
                      name="firstName"
                      variant="outlined"
                      onChange={handleChange}
                      onBlur={handleBlur}
                      fullWidth
                      style={fieldStyles}
                      error={touched.firstName && Boolean(errors.firstName)}
                      helperText = {touched.firstName && errors.firstName}
                    />
                  </div>
                  <div>
                    <TextField
                      id="lastName"
                      label="Last Name"
                      name="lastName"
                      variant="outlined"
                      onChange={handleChange}
                      onBlur={handleBlur}
                      fullWidth
                      style={fieldStyles}
                      error={touched.lastName && Boolean(errors.lastName)}
                      helperText = {touched.lastName && errors.lastName}
                    />
                  </div>
                  <div>
                    <TextField
                      id="userName"
                      label="UserName"
                      name="userName"
                      variant="outlined"
                      onChange={handleChange}
                      onBlur={handleBlur}
                      fullWidth
                      style={fieldStyles}
                      error={touched.userName && Boolean(errors.userName)}
                      helperText = {touched.userName && errors.userName}
                    />
                   
                  </div>
                  <div>
                    <TextField
                      id="email"
                      label="Email"
                      name="email"
                      variant="outlined"
                      onChange={handleChange}
                      onBlur={handleBlur}
                      fullWidth
                      style={fieldStyles}
                      error={touched.email && Boolean(errors.email)}
                      helperText = {touched.email && errors.email}
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
                  <div>
                    <FormControlLabel
                      control={
                        <Checkbox
                          checked={registerAsAuthor}
                          onChange={(e) => setRegisterAsAuthor(e.target.checked)}
                          name="registerAsAuthor"
                          color="primary"
                        />
                      }
                      label="Register as Author"
                    />
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
    </div>
  );
};

export default RegisterModalForm;
