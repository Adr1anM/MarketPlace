import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import {BrowserRouter, RouterProvider, createBrowserRouter} from "react-router-dom";
import ArtworksPage from './components/pages/ArtworksPage.tsx';
import Layout from './components/pages/Layout.tsx';
import { AuthProvider } from './contexts/AuthContext.tsx';
import  ErrorPage  from './components/ErrorPage.tsx';
import ArtistsPage from './components/pages/ArtistsPage.tsx';
import HomePage from './components/pages/HomePage'

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
