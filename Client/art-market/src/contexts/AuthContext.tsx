import { createContext, useContext, useState, ReactNode, FC  } from "react";
import axios from "axios";
import AuthContextType from "./AuthContexType";
//import { useNavigate } from "react-router-dom";


const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: FC<{children: ReactNode}> = ({children}) => {
    const [token, setToken] = useState<string | null>(null);
   // const navigate = useNavigate();
  
    const login = (newToken:string) => {
        setToken(newToken);
        localStorage.setItem('token', newToken);
        axios.defaults.headers.common['Authorization'] = 'Bearer ${newToken}';
    }

    const logout = () =>{
        setToken(null);
        localStorage.removeItem('token');
        delete axios.defaults.headers.common['Authorization'];
       // navigate('/login');
    }

    return (
        <AuthContext.Provider value={{ token, login, logout }}>
          {children}
        </AuthContext.Provider>
      );
};

export const useAuth = () =>{
    const context = useContext(AuthContext);
    if (context === undefined) {
        throw new Error('useAuth must be used within an AuthProvider');
      }
    return context;  
};