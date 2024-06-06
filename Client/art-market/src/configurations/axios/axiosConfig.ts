import axios from "axios";
import { useAuth } from "../../contexts/AuthContext";

axios.defaults.baseURL = 'https://localhost:7096/api';

axios.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem('token');
        if(token){
            config.headers['Authorization'] = 'Bearer ${token}';
        }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);


axios.interceptors.response.use(
    (response) => response,
    (error) => {
        if(error.response && error.response.status == 401){
            const {logout} = useAuth();
            logout();
        }
        return Promise.reject(error);
    }
);


export default axios;
