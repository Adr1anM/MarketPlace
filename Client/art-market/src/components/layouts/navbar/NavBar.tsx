import React, { useState } from 'react';
import '../styles/navBar.css';
import LogInModal from './navbarLogin/LogInModal';
import MegaMenu from './megaMenues/MegaMenu';
import {artistsSections , artworksSections} from '../../../dummyDataStore/megamenyData'
import {NavLink} from 'react-router-dom'
import { useAuth } from '../../../contexts/AuthContext';
import ProfileMenu from './navbarLogin/ProfileMenu';
import toast from 'react-hot-toast';


const NavBar: React.FC = () => {
  const { logout } = useAuth();
  const [showArtistsMegaMenu, setShowArtistsMegaMenu] = useState(false);
  const [showArtworksMegaMenu, setShowArtworksMegaMenu] = useState(false);


  function handleLoggOut(){
    toast.success("Logged Out")
    logout();
  }
  
  return (
      <div className="header-menu-wrapper-desktop" style={{backgroundColor: "red"}}>
        <header className="artshop-header" >
          <div className="header-content">
            <div className="header-title">
              <NavLink style={{color: 'black' , fontWeight: '700'}} to="/home">ArtIs</NavLink>
            </div>
            <div className="header-search-wrapper">
              <input className="search-input" type="text" placeholder="Search..." />
            </div>
            <div className="signin-section">
              <LogInModal />
            </div>
          </div>
          <ProfileMenu handleLogout={handleLoggOut} />
        </header>
        <nav className="nav">
          <div className="nav-container">
            <div className="nav-container-section">
              <div 
                className="nav-content" 
                onMouseEnter={() => setShowArtistsMegaMenu(true)}
                onMouseLeave={() => setShowArtistsMegaMenu(false)}
                onClick={() => setShowArtistsMegaMenu(false)}
              >
                <NavLink to="/artists">Artists</NavLink>
                {showArtistsMegaMenu && (
                  <MegaMenu sections={artistsSections} className="artists-megamenu" isOpen={showArtistsMegaMenu} />
                )}
              </div>
              <div 
                className="nav-content" 
                onMouseEnter={() => setShowArtworksMegaMenu(true)}
                onMouseLeave={() => setShowArtworksMegaMenu(false)}
                onClick={() => setShowArtworksMegaMenu(false)}
              >
                <NavLink to="/artworks">Artworks</NavLink>
                {showArtworksMegaMenu && (
                  <MegaMenu sections={artworksSections} className="artworks-megamenu" isOpen={showArtworksMegaMenu} />
                )}
              </div>
                  <div className="nav-content" ><NavLink to="/collections">Collections</NavLink></div>
                  <div className="nav-content" ><NavLink to="/auctions">Auctions</NavLink></div>
              </div>

              <div className="nav-container-section">
                  <div className="nav-content" ><NavLink to="/buy-art">Buy Art</NavLink></div>
                  <div className="nav-content" ><NavLink to="/media">Media</NavLink></div>
                  <div className="nav-content" ><NavLink to="/seller">Seller</NavLink></div>
                  <div className="nav-content" ><NavLink to="/about-us">About-Us</NavLink></div>
              </div>
          </div>                   
        </nav>
      </div>
  );
};

export default NavBar;
