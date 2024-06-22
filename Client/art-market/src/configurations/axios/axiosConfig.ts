import axios from "axios";
import toast from "react-hot-toast";
axios.defaults.baseURL = 'https://localhost:7096/api';

axios.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem('token');
        if(token){
            config.headers['Authorization'] = `Bearer ${token}`;
        }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);


axios.interceptors.response.use(
    response => response,
    error => {
        if (error.response) {
            const { status, data } = error.response;
            if (status === 409 && data.type === "UserNameDuplicate") {
                toast.error("User already exists. Please choose a different username.");
            } else {
           
              //  toast.error("An unexpected error occurred.");
            }
        } else {
            //toast.error("Network error. Please try again later.");
        }
        return Promise.reject(error);
    }
);


export default axios;
