import { AuthProvider } from './contexts/AuthContext';
import LogInComponent from './components/LogIn';
import { Route, Routes } from 'react-router-dom';
import { ErrorPage } from './components/ErrorPage';
import NavBar from './components/layouts/navbar/NavBar';
import './App.css';
import LogInForm from './components/layouts/navbar/LoginForm';
import Dropdown from './Dropdown';

function App() {
  return (
    <AuthProvider>
      <NavBar />
      <div className="content-wrapper">
        <Routes>
          <Route path="/" element={<LogInForm />} />
          <Route path="/home" element={<LogInComponent />} />
          <Route path="/products" element={<LogInComponent />} />
          <Route path="/dropdown" element = {<Dropdown/>}/>
          <Route path="*" element={<ErrorPage />} />
        </Routes>
      </div>
    </AuthProvider>
  );
}

export default App;
