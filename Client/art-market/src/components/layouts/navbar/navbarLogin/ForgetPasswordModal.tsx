import { Box, Button, IconButton, InputAdornment, Modal, TextField, Typography } from "@mui/material";
import { FC, useEffect, useState } from "react";
import CloseIcon from '@mui/icons-material/Close';
import { ErrorMessage, Form, Formik } from "formik";
import * as Yup from 'yup';
import { Visibility } from "@mui/icons-material";

const validationSchema = Yup.object({
    email: Yup.string().required('Email is required'),
   
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
     
interface ForgetPasswordModalProps {
    isOpened: boolean;
    onClose: () => void;
}
    
const ForgetPasswordModal: FC<ForgetPasswordModalProps> = ({isOpened , onClose}) => {

  const [open, setOpen] = useState(isOpened);

  useEffect(() => {
    setOpen(isOpened);
  }, [isOpened]);
  
  const initialValues = {
    email: '',
  };

  const handleClose = () =>{
    setOpen(false);
    onClose();
  }

  const CustomErrorComponent = ({ children }: { children: React.ReactNode }) => (
    <div style={{ color: 'red' }}>{children}</div>
  );

  const handleLogin = async (values:any) =>{
    try{
        
    } catch (error) {
       
    }
  }
  
    return(
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
          <Typography id="modal-modal-title" variant="h5"  style={{color: 'black' , marginBottom: '16px'}}>
            Resert your password
          </Typography>
          <Formik
            initialValues={initialValues}
            validationSchema={validationSchema}
            onSubmit= {handleLogin}
          >
            {({ isSubmitting, handleChange, handleBlur }) => (
              <Form>
                <Box sx={{ '& .MuiTextField-root': {  marginBottom: '16px' } }}>
                  <div>
                    <TextField
                      id="email"
                      label="Email"
                      name="email"
                      variant="outlined"
                      onChange={handleChange}
                      onBlur={handleBlur}
                      fullWidth
                    />
                    <ErrorMessage name="username" render={(msg) => <CustomErrorComponent>{msg}</CustomErrorComponent>} />
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
}

export default ForgetPasswordModal;