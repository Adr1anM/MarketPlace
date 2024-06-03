import { Suspense } from "react"
import { Route, Routes } from "react-router-dom";
import LogInForm from "../components/layouts/navbar/navbarLogin/LoginForm";
import  ErrorPage  from "../components/ErrorPage";
import LogInComponent from '../components/LogIn';
import ArtworksPage from "../components/pages/ArtworksPage";


const AppRoutes = () =>{
    return(
        <Suspense fallback={<div>Loading...</div>}>
          <Routes>
            <Route path="/" element={<LogInForm />} />
            <Route path="/home" element={<LogInComponent />} />
            <Route path="/products" element={<LogInComponent />} />
            <Route path="/artworks" element={<ArtworksPage />} />
            <Route path="*" element={<ErrorPage />} />
          </Routes>
        </Suspense>
    );
}   

export default AppRoutes;