// src/components/LoginComponent.tsx
import React, { useState } from 'react';
import axios from '../../configurations/axios/axiosConfig'; 
import { useAuth } from '../../contexts/AuthContext';

interface LogInModel{
    username: string | null;
    password: string | null;
}

const LoginComponent: React.FC = () => {
  const { login } = useAuth();
  const [userName, setUsername] = useState('');
  const [password, setPassword] = useState('');

  const handleLogin = async () => {
    try {
      const command:LogInModel = {
        username : userName,
        password : password
      }
      const response = await axios.post('/Account/login',  command );
      const token = response.data;
      console.log(userName,password);
      console.log(token);
      console.log(axios.defaults.headers);
      login(token);
    } catch (error) {
      console.error('Failed to login', error);
    }
  };

  return (
    <div>
      <input
        type="email"
        value={userName}
        onChange={(e) => setUsername(e.target.value)}
        placeholder="Email"
      />
      <input
        type="password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        placeholder="Password"
      />
      <button onClick={handleLogin}>Login</button>
    </div>
  );
};

export default LoginComponent;
