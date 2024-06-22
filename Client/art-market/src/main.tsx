import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import {RouterProvider, createBrowserRouter} from "react-router-dom";
import ArtworksPage from './components/pages/ArtworksPage.tsx';
import Layout from './components/pages/Layout.tsx';
import { AuthProvider } from './contexts/AuthContext.tsx';
import  ErrorPage  from './components/ErrorPage.tsx';
import ArtistsPage from './components/pages/ArtistsPage.tsx';
import HomePage from './components/pages/HomePage'
import ProfilePage from './components/pages/ProfilePage.tsx';
import { CssBaseline, ThemeProvider, createTheme } from '@mui/material';
import DetailedArtworkPage from './components/pages/DetailedArtworkPage.tsx';
import RegisterPage from './components/auth/RegisterPage.tsx';
import ShoppingCartPage from './components/pages/ShoppingCartPage.tsx';

const router = createBrowserRouter([{
  path: '/',
  element: <Layout />,
  errorElement: <ErrorPage />,
  children: [
    {
      path: '/',
      element: <HomePage />,
    },
    {
      path: '/home',
      element: <HomePage />,
    },
    {
      path: '/artworks',
      element: <ArtworksPage />,
    },
    {
      path: '/artists',
      element: <ArtistsPage />,
    },
    {
      path: '/profile/:id?',
      element: <ProfilePage />,
    },
    {
      path: '/register',
      element: <RegisterPage />,
    },
    {
      path: "/artwork/:id",
      element: <DetailedArtworkPage/>
    },
    {
      path: "/author/:authorId",
      element: <ProfilePage/>
    },
    {
      path: '/shopping-cart',
      element: <ShoppingCartPage />, 
    },
    {
      path: '*',
      element: <ErrorPage />, 
    },
  ],
},])


const theme = createTheme({
  palette: {
    primary: {
      main: '#1976d2',
    },
    secondary: {
      main: '#dc004e',
    },
  },
});

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <AuthProvider>
        <RouterProvider router={router}/>
      </AuthProvider>
    </ThemeProvider>
   
  </React.StrictMode>,
)

