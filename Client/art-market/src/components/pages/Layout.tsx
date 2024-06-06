import { Outlet } from "react-router-dom";
import NavBar from "../layouts/navbar/NavBar";
import Footer from "../layouts/Footer";
import { Toaster } from "react-hot-toast";

export default function Layout(){
    return( 
    <div style={{display: "flex", flexDirection: "column", height: "100%", width: "100%" }}>
        <NavBar/>
        <Toaster position="top-center" reverseOrder={false}/>
        <Outlet/>
        <Footer/>
       
    </div>
    );
}