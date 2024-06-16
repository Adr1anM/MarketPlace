import { createContext, useContext, useState, ReactNode, FC, useEffect  } from "react";
import axios from "axios";
import AuthContextType from "./AuthContexType";
import { User } from "../types/types";


const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: FC<{children: ReactNode}> = ({children}) => {
    const [token, setToken] = useState<string | null>(null);
    const [isLoggedIn, setLoggedIn] = useState(() => !!localStorage.getItem('token'));

    const [user, setUser] = useState<User | undefined>(() => {
      const storedUser = localStorage.getItem('user');
      return storedUser ? JSON.parse(storedUser) : undefined;
    });
    
    
    useEffect(() => {
        if (token) {
          axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        }
    }, [token]);

    useEffect(() => {
      if (user) {
          localStorage.setItem('user', JSON.stringify(user));
      } else {
          localStorage.removeItem('user');
      }
    }, [user]);
  
    const login = (newToken:string) => {
        setToken(newToken);
        localStorage.setItem('token', newToken);
        axios.defaults.headers.common['Authorization'] = `1Bearer ${newToken}`;
        setLoggedIn(true);
    }

    const logout = () =>{
        setToken(null);
        localStorage.removeItem('token');
        localStorage.removeItem('user');

        delete axios.defaults.headers.common['Authorization'];
        setLoggedIn(false);
    }

 
    return (
        <AuthContext.Provider value={{ token, login, logout, isLoggedIn, user, setUser }}>
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