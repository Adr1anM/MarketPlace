import React, { useState } from 'react';
import { Box, Button, Typography, TextField, FormControlLabel, Checkbox, Grid, Paper, Stack } from '@mui/material';
import { Field, FieldProps, Form, Formik, FormikHelpers } from 'formik';
import { useAuth } from '../../contexts/AuthContext';
import axios from '../../configurations/axios/axiosConfig'; 
import { initialValues, validationSchema } from './formValidation';
import CountrySelect from './CountrySelect';
import { DemoContainer } from '@mui/x-date-pickers/internals/demo';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { DatePicker, LocalizationProvider } from '@mui/x-date-pickers';
import InputFileUpload from '../pages/profile/InputFileUpload';
import { AuthorData, RegisterData } from '../../types/types';
import useAuthors from '../../zsm/stores/useAuthors';
import toast from 'react-hot-toast';
import './registerPage.css'

const RegistrationPage: React.FC = () => {
    const { login, setUser } = useAuth();
    const authorStore = useAuthors()
    const [registerAsAuthor, setRegisterAsAuthor] = useState(false);
    const [imageData, setImageData] = useState<string | null>(null);

    const handleRegister = async (values: typeof initialValues, { setSubmitting, resetForm }: FormikHelpers<typeof initialValues>) => {
        const { firstName, lastName, userName, email, password, biography, country, socialMediaLinks, phoneNumber } = values;
        const role = registerAsAuthor ? 'Author' : 'User';
        const registerCommand: RegisterData = {
            firstName: firstName,
            lastName: lastName,
            userName: userName,
            email: email,
            password: password,
            role: role
        };

        setSubmitting(true);
        toast.promise(
            axios.post('/Account/register', registerCommand)
            .then(async response => {
              const token = response.data;
              login(token);

              try {
                const userResponse = await axios.get('/Account/user'); 
                const user = userResponse.data;
                setUser(user);
                localStorage.setItem('user', JSON.stringify(user)); 
                if (registerAsAuthor) {
                
                    const birthDate =values.birthDate.toISOString();
                    const numberOfPosts = values.numberOfPosts
                    const profileImage = imageData;
                    console.log("profileImage",profileImage)
                    const authorData: AuthorData = { userId: user?.id , biography, country, birthDate ,socialMediaLinks,numberOfPosts, phoneNumber ,profileImage };
                    const authorResponse = await authorStore.createAuthor(authorData);
                    console.log(authorData);
                    console.log(authorResponse);
                }
            } catch (error) {
                console.error('Failed to fetch user details', error);
            }
            finally{
                resetForm();
                setRegisterAsAuthor(false);
                setSubmitting(false);
            }
            }),
            {
              loading: 'Registering...',
              success: <b>Registering successfully!</b>,
              error: <b>Failed to Register.</b>,
            }
          ).catch(error => {
            console.error('Failed to Register', error);
          })
    };

    function handleCancel() {
        setImageData(null);
    }

    const handleUpload = (file: File) => {
        try {
          const reader = new FileReader();
          reader.readAsDataURL(file);
          reader.onload = () => {
            const base64String = reader.result as string;
            console.log(base64String);
            setImageData(base64String);
          };
        } catch (error) {
          console.error('Error uploading image:', error);
        }
      };

    return (
        <Box className = "main-box">
            <Grid container spacing={8} sx={{ width: '90%', maxWidth: 1600 }}>
             
                <Grid item xs={12} md={6}>
                    <Box className = "image-box">
                        <img
                            src="https://art.rtistiq.com/static/media/buyer_ad_image.f12005fc.png"
                            alt="Registration Background"
                            style={{ maxWidth: '100%', height: 'auto' }}
                        />
                    </Box>
                </Grid>

             
                <Grid item xs={12} md={6}>
                    <Paper className='main-form-box' elevation={3}>
                        <Typography variant="h4" gutterBottom>
                            Register
                        </Typography>
                        <Formik
                            initialValues={initialValues}
                            validationSchema={validationSchema}
                            onSubmit={handleRegister}
                        >
                            {({ isSubmitting, handleChange, handleBlur,setFieldValue, touched, errors, values }) => (
                                <Form>
                                    <Grid container spacing={2}>
                                       {(registerAsAuthor && imageData !== null ) &&  
                                         <Grid className='logo-grid-item' item xs={12} >
                                            <Box
                                                component="img"
                                                sx={{
                                                height: 150,
                                                width: 150,
                                                borderRadius: '50%',
                                                objectFit: 'cover'
                                                }}
                                                alt="Author profile"
                                                src={`data:image/jpeg;base64${imageData}`}
                                            />
                                            
                                        </Grid>
                                        }
                                        {(registerAsAuthor && imageData === null) &&  
                                         <Grid className='logo-grid-item' item xs={12} >
                                            <Box
                                                component="img"
                                                sx={{
                                                height: 150,
                                                width: 150,
                                                borderRadius: '50%',
                                                objectFit: 'cover'
                                                }}
                                                alt="Author profile"
                                                src={"https://upload.wikimedia.org/wikipedia/commons/a/a2/Person_Image_Placeholder.png"}
                                            />
                                            
                                        </Grid>
                                        }
                                        { registerAsAuthor &&
                                        <Grid className='logo-grid-item' item xs={12} >
                                            <Stack sx={{marginTop: '15px'}} direction="row" gap="10px">
                                                <Button onClick={handleCancel}>Cancel</Button>
                                                <InputFileUpload onUpload={handleUpload} />
                                            </Stack>  
                                        </Grid>
                                        }
                                        <Grid item xs={12} sm={6}>
                                            <TextField
                                                id="firstName"
                                                label="First Name"
                                                name="firstName"
                                                variant="outlined"
                                                fullWidth
                                                onChange={handleChange}
                                                onBlur={handleBlur}
                                                value={values.firstName}
                                                error={touched.firstName && Boolean(errors.firstName)}
                                                helperText={touched.firstName && errors.firstName}
                                            />
                                        </Grid>
                                        <Grid item xs={12} sm={6}>
                                            <TextField
                                                id="lastName"
                                                label="Last Name"
                                                name="lastName"
                                                variant="outlined"
                                                fullWidth
                                                onChange={handleChange}
                                                onBlur={handleBlur}
                                                value={values.lastName}
                                                error={touched.lastName && Boolean(errors.lastName)}
                                                helperText={touched.lastName && errors.lastName}
                                            />
                                        </Grid>
                                        <Grid item xs={12}>
                                            <TextField
                                                id="userName"
                                                label="Username"
                                                name="userName"
                                                variant="outlined"
                                                fullWidth
                                                onChange={handleChange}
                                                onBlur={handleBlur}
                                                value={values.userName}
                                                error={touched.userName && Boolean(errors.userName)}
                                                helperText={touched.userName && errors.userName}
                                            />
                                        </Grid>
                                        <Grid item xs={12}>
                                            <TextField
                                                id="email"
                                                label="Email"
                                                name="email"
                                                variant="outlined"
                                                fullWidth
                                                onChange={handleChange}
                                                onBlur={handleBlur}
                                                value={values.email}
                                                error={touched.email && Boolean(errors.email)}
                                                helperText={touched.email && errors.email}
                                            />
                                        </Grid>
                                        <Grid item xs={12}>
                                            <TextField
                                                id="password"
                                                label="Password"
                                                name="password"
                                                variant="outlined"
                                                type="password"
                                                fullWidth
                                                onChange={handleChange}
                                                onBlur={handleBlur}
                                                value={values.password}
                                                error={touched.password && Boolean(errors.password)}
                                                helperText={touched.password && errors.password}
                                            />
                                        </Grid>
                                        <Grid item xs={12}>
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
                                        </Grid>
                                        {registerAsAuthor && (
                                            <>
                                                <Grid item xs={12}>
                                                    <TextField
                                                        id="biography"
                                                        label="Biography"
                                                        name="biography"
                                                        variant="outlined"
                                                        fullWidth
                                                        onChange={handleChange}
                                                        onBlur={handleBlur}
                                                        value={values.biography}
                                                        error={touched.biography && Boolean(errors.biography)}
                                                        helperText={touched.biography && errors.biography}
                                                    />
                                                </Grid>
                                                <Grid item xs={12}>
                                                <Grid item xs={12}>
                                                    <Field name="country">
                                                        {({ field, form }: FieldProps) => {
                                                            const error = form.touched[field.name] && Boolean(form.errors[field.name]);
                                                            const helperText = form.touched[field.name] && form.errors[field.name] 
                                                                ? String(form.errors[field.name]) 
                                                                : undefined;

                                                            return (
                                                                <CountrySelect
                                                                    value={field.value}
                                                                    onChange={(value) => form.setFieldValue(field.name, value)}
                                                                    error={error}
                                                                    helperText={helperText}
                                                                />
                                                            );
                                                        }}
                                                    </Field>
                                                </Grid>
                                                </Grid>
                                                <Grid item xs={12}>
                                                <LocalizationProvider dateAdapter={AdapterDayjs}>
                                                <DemoContainer components={['DatePicker']}>
                                                    <DatePicker
                                                        label="Birth Date"
                                                        value={values.birthDate}
                                                        onChange={(newValue) => setFieldValue('birthDate', newValue)}
                                                        sx={{width: '100%'}}
                                                    />
                                                </DemoContainer>
                                                </LocalizationProvider>
                                                </Grid>
                                                <Grid item xs={12}>
                                                    <TextField
                                                        id="socialMediaLinks"
                                                        label="Social Media Links"
                                                        name="socialMediaLinks"
                                                        variant="outlined"
                                                        fullWidth
                                                        onChange={handleChange}
                                                        onBlur={handleBlur}
                                                        value={values.socialMediaLinks}
                                                        error={touched.socialMediaLinks && Boolean(errors.socialMediaLinks)}
                                                        helperText={touched.socialMediaLinks && errors.socialMediaLinks}
                                                    />
                                                </Grid>
                                                <Grid item xs={12}>
                                                    <TextField
                                                        id="phoneNumber"
                                                        label="Phone Number"
                                                        name="phoneNumber"
                                                        variant="outlined"
                                                        fullWidth
                                                        onChange={handleChange}
                                                        onBlur={handleBlur}
                                                        value={values.phoneNumber}
                                                        error={touched.phoneNumber && Boolean(errors.phoneNumber)}
                                                        helperText={touched.phoneNumber && errors.phoneNumber}
                                                    />
                                                </Grid>
                                                
                                            </>
                                        )}
                                        <Grid item xs={12}>
                                            <Button
                                                type="submit"
                                                variant="contained"
                                                color="primary"
                                                disabled={isSubmitting}
                                                fullWidth
                                            >
                                                Submit
                                            </Button>
                                        </Grid>
                                    </Grid>
                                </Form>
                            )}
                        </Formik>
                    </Paper>
                </Grid>
            </Grid>
        </Box>
    );
};

export default RegistrationPage;
