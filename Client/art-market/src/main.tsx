import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import './index.css'
import {BrowserRouter, RouterProvider, createBrowserRouter} from "react-router-dom";
import ArtworksPage from './components/pages/ArtworksPage.tsx';
import Layout from './routes/Layout.tsx';
import { AuthProvider } from './contexts/AuthContext.tsx';
import  ErrorPage  from './components/ErrorPage.tsx';
import LogInComponent from './components/LogIn.tsx';
import ArtistsPage from './components/pages/ArtistsPage.tsx';

const router = createBrowserRouter([{
  path: '/',
  element: <Layout />,
  errorElement: <ErrorPage />,
  children: [
    {
      path: '/',
      element: <ArtworksPage />,
    },
    {
      path: '/home',
      element: <LogInComponent />,
    },
    {
      path: '/products',
      element: <LogInComponent />,
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
      path: '*',
      element: <ErrorPage />,
    },
  ],
},])

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <AuthProvider>
      <RouterProvider router={router}/>
    </AuthProvider>
  </React.StrictMode>,
)
