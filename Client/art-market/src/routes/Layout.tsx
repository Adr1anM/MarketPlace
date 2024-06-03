import { Outlet } from "react-router-dom";
import NavBar from "../components/layouts/navbar/NavBar";

export default function Layout(){
    return( 
    <div style={{display: "flex", flexDirection: "column", height: "100%", width: "100%" }}>
        <NavBar/>
        <Outlet/>
    </div>
    );
}