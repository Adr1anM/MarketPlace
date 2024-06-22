import dayjs from 'dayjs';
import * as Yup from 'yup';

export const validationSchema = Yup.object({
    firstName: Yup.string()
      .min(2, 'First Name must be at least 2 characters long')
      .required('First Name is required'),
    lastName: Yup.string()
      .min(2, 'Last Name must be at least 2 characters long')
      .required('Last Name is required'),
    userName: Yup.string()
      .min(5, 'Username must be at least 5 characters long')
      .max(50, 'Username is too long')
      .matches(/[A-Z]/, 'Username must contain at least one uppercase letter')
      .required('Username is required'),
    email: Yup.string().email('Invalid email').required('Email is required'),
    password: Yup.string()
      .min(6, 'Password must be at least 6 characters long')
      .required('Password is required'),
    biography: Yup.string().test(
      'biographyRequirement',
      'Biography must be less than 400 characters',
      (value, context) => {
        if (context.parent.registerAsAuthor) {
          return value === undefined || value.length <= 400;
        }
        return true;
      }
    ),
    country: Yup.string().test(
      'countryRequirement',
      'Country must be less than 100 characters',
      (value, context) => {
        if (context.parent.registerAsAuthor) {
          return value === undefined || value.length <= 100;
        }
        return true;
      }
    ),
    birthDate: Yup.date().test(
      'birthDateRequirement',
      'Birth Date is required',
      (value, context) => {
        if (context.parent.registerAsAuthor) {
          return value !== undefined;
        }
        return true;
      }
    ),
    socialMediaLinks: Yup.string().test(
      'socialMediaLinksRequirement',
      'Social Media Links must be less than 200 characters',
      (value, context) => {
        if (context.parent.registerAsAuthor) {
          return value === undefined || value.length <= 200;
        }
        return true;
      }
    ),
    phoneNumber: Yup.string().test(
      'phoneNumberRequirement',
      'Phone Number is required',
      (value, context) => {
        if (context.parent.registerAsAuthor) {
          return value !== undefined;
        }
        return true;
      }
    ),
  });
  
export const initialValues = {
    firstName: '',
    lastName: '',
    userName: '',
    email: '',
    password: '',
    biography: '',
    country: '',
    birthDate:  dayjs(),
    socialMediaLinks: '',
    numberOfPosts: 0,
    phoneNumber: '',
    profileImage: '',
    registerAsAuthor: false,
  };