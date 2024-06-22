import * as Yup from 'yup';

export interface LogInModel{
    username: string;
    password: string;
  }
  
  export const validationSchema = Yup.object({
    username: Yup.string().required('Username is required'),
    password: Yup.string().required('Password is required'),
  });
  
  export const style = {
    position: 'absolute' as 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 350,
    bgcolor: 'white',
    boxShadow: 24,
    p: 3,
  };
  
  
 export const fieldStyles = {
      backgroundColor: 'transparent',
      boxShadow: 'none',
  
  }
  
  export const buttonStyle = {
      backgroundColor: 'white',
      color: 'black',
      outline: 'none',
      height: '10px',
  }
  