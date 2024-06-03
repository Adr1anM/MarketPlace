import { AuthProvider } from './contexts/AuthContext';
import NavBar from './components/layouts/navbar/NavBar';
import './App.css';
import AppRoutes from './routes/routes';

function App() {
  return (
    <AuthProvider>
      <div style={{display: "grid"}}>
      <NavBar />
      <AppRoutes/>  
      </div>
    </AuthProvider>
  );
}

export default App;
