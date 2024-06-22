import React, { useState } from 'react';
import '../styles/navBar.css';
import LogInModal from './navbarLogin/LogInModal';
import MegaMenu from './megaMenues/MegaMenu';
import {artistsSections , artworksSections} from '../../../dummyDataStore/megamenyData'
import {NavLink} from 'react-router-dom'
import { useAuth } from '../../../contexts/AuthContext';
import ProfileMenu from './navbarLogin/ProfileMenu';
import toast from 'react-hot-toast';
import { useNavigate } from 'react-router-dom';
import { Badge, BadgeProps, Box, Button, IconButton, Link, Typography, styled } from '@mui/material';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
import "../../../components/pages/pagesStyles/ButtonStyles.css"
import RegistrationPage from '../../auth/RegisterPage';
import { Link as RouterLink } from 'react-router-dom'; 
import useShoppingCart from '../../../zsm/stores/useShoppingCart';


const StyledBadge = styled(Badge)<BadgeProps>(({ theme }) => ({
  '& .MuiBadge-badge': {
    right: -3,
    top: 13,
    border: `2px solid ${theme.palette.background.paper}`,
    padding: '0 4px',
  },
}));

const NavBar: React.FC = () => {
  const { logout } = useAuth();
  const navigate = useNavigate();
  const { getCartItemCount } = useShoppingCart();
  const itemCount = getCartItemCount();
  const [showArtistsMegaMenu, setShowArtistsMegaMenu] = useState(false);
  const [showArtworksMegaMenu, setShowArtworksMegaMenu] = useState(false);
  const [loginModal, setloginModal] = useState(false);

  

  const handleArtworkLinkClick = (priceRange: string) => {
    const pageIndex = 0; 
    const queryString = `?pageIndex=${pageIndex}&priceRange=${priceRange}`;

    navigate(`/artworks${queryString}`);
  };
  

  function handleLoggOut(){
    toast.success("Logged Out")
    logout();
  }
  
  function handleOpen() {
    setloginModal(true);
  }

  function handleLoginModalClose() {
    setloginModal(false);
  }

  return (
      <Box className="header-menu-wrapper-desktop" style={{backgroundColor: "red"}}>
        <header className="artshop-header" >
          <Box className="header-content">
            <Box className="header-title">
              <NavLink style={{color: 'black' , fontWeight: '700'}} to="/home">ArtIs</NavLink>
            </Box>
            <Box className="header-search-wrapper">
              <input className="search-input" type="text" placeholder="Search..." />
            </Box>
            <Box  marginRight={5}>
              <IconButton href='/shopping-cart' className = "button" aria-label="cart">
                <StyledBadge badgeContent={itemCount} color="secondary">
                 <ShoppingCartIcon />
                </StyledBadge>
              </IconButton>
            </Box>
            <Box className="signin-section">
              <Button style={{ textTransform: 'capitalize', fontSize: '17px' , outline: 'none', color: 'black' }} onClick={handleOpen}>
                SignIn/Register
              </Button>
            </Box>
          </Box>
          <ProfileMenu handleLogout={handleLoggOut} />
        </header>
        <nav className="nav">
          <Box className="nav-container">
            <Box className="nav-container-section">
              <Box 
                className="nav-content" 
                onMouseEnter={() => setShowArtistsMegaMenu(true)}
                onMouseLeave={() => setShowArtistsMegaMenu(false)}
                onClick={() => setShowArtistsMegaMenu(false)}
              >
                <NavLink to="/artists">Artists</NavLink>
                {showArtistsMegaMenu && (
                  <MegaMenu sections={artistsSections} className="artists-megamenu" isOpen={showArtistsMegaMenu} handleArtworkLinkClick={handleArtworkLinkClick} />
                )}
              </Box>
              <Box 
                className="nav-content" 
                onMouseEnter={() => setShowArtworksMegaMenu(true)}
                onMouseLeave={() => setShowArtworksMegaMenu(false)}
                onClick={() => setShowArtworksMegaMenu(false)}
              >
                <NavLink to="/artworks">Artworks</NavLink>
                {showArtworksMegaMenu && (
                  <MegaMenu className="artworks-megamenu" handleArtworkLinkClick={handleArtworkLinkClick} sections={artworksSections}  isOpen={showArtworksMegaMenu} />
                )}
              </Box>
                  <Box className="nav-content" ><NavLink to="/collections">Collections</NavLink></Box>
                  <Box className="nav-content" ><NavLink to="/auctions">Auctions</NavLink></Box>
              </Box>

              <Box className="nav-container-section">
                  <Box className="nav-content" ><NavLink to="/buy-art">Buy Art</NavLink></Box>
                  <Box className="nav-content" ><NavLink to="/media">Media</NavLink></Box>
                  <Box className="nav-content" ><NavLink to="/seller">Seller</NavLink></Box>
                  <Box className="nav-content" ><NavLink to="/about-us">About-Us</NavLink></Box>
              </Box>
          </Box>                   
        </nav>
        <LogInModal isOpened={loginModal} onClose={handleLoginModalClose} title='Log in to collect art by the world’s leading artists'/>
      </Box>
  );
};

export default NavBar;
