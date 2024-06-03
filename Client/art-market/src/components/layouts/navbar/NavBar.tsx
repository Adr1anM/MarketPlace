import React, { useState, useRef, useEffect } from 'react';
import './styles/navBar.css';
import Avatar from '@mui/material/Avatar';
import Menu from '@mui/material/Menu';
import MenuItem from '@mui/material/MenuItem';
import ListItemIcon from '@mui/material/ListItemIcon';
import Divider from '@mui/material/Divider';
import IconButton from '@mui/material/IconButton';
import Tooltip from '@mui/material/Tooltip';
import PersonAdd from '@mui/icons-material/PersonAdd';
import Settings from '@mui/icons-material/Settings';
import Logout from '@mui/icons-material/Logout';
import { Box } from '@mui/material';
import LogInModal from './navbarLogin/LogInModal';
import MegaMenu from './megaMenues/MegaMenu';
import {artistsSections , artworksSections} from '../../../dummyDataStore/megamenyData'
import {Link, NavLink} from 'react-router-dom'


const NavBar: React.FC = () => {
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const [showArtistsMegaMenu, setShowArtistsMegaMenu] = useState(false);
  const [showArtworksMegaMenu, setShowArtworksMegaMenu] = useState(false);
  const open = Boolean(anchorEl);

  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };


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
          <Box sx={{ display: 'flex', alignItems: 'center', textAlign: 'center' }}>
            <Tooltip title="Account settings">
              <IconButton
                onClick={handleClick}
                size="small"
                sx={{ ml: 2 }}
                aria-controls={open ? 'account-menu' : undefined}
                aria-haspopup="true"
                aria-expanded={open ? 'true' : undefined}
                style={{ outline: 'none' }}
              >
                <Avatar sx={{ width: 32, height: 32 }}>M</Avatar>
              </IconButton>
            </Tooltip>
          </Box>
          <Menu
            anchorEl={anchorEl}
            id="account-menu"
            open={open}
            onClose={handleClose}
            onClick={handleClose}
            PaperProps={{
              elevation: 0,
              sx: {
                overflow: 'visible',
                filter: 'drop-shadow(0px 2px 8px rgba(0,0,0,0.32))',
                mt: 1.5,
                '& .MuiAvatar-root': {
                  width: 32,
                  height: 32,
                  ml: -0.5,
                  mr: 1,
                },
                '&::before': {
                  content: '""',
                  display: 'block',
                  position: 'absolute',
                  top: 0,
                  right: 14,
                  width: 10,
                  height: 10,
                  bgcolor: 'background.paper',
                  transform: 'translateY(-50%) rotate(45deg)',
                  zIndex: 0,
                },
              },
            }}
            transformOrigin={{ horizontal: 'right', vertical: 'top' }}
            anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
          >
            <MenuItem onClick={handleClose}>
              <Avatar /> Profile
            </MenuItem>
            <MenuItem onClick={handleClose}>
              <Avatar /> My account
            </MenuItem>
            <Divider />
            <MenuItem onClick={handleClose}>
              <ListItemIcon>
                <PersonAdd fontSize="small" />
              </ListItemIcon>
              Add another account
            </MenuItem>
            <MenuItem onClick={handleClose}>
              <ListItemIcon>
                <Settings fontSize="small" />
              </ListItemIcon>
              Settings
            </MenuItem>
            <MenuItem onClick={handleClose}>
              <ListItemIcon>
                <Logout fontSize="small" />
              </ListItemIcon>
              Logout
            </MenuItem>
          </Menu>
        </header>
        <nav className="nav">
          <div className="nav-container">
            <div className="nav-container-section">
              <div 
                className="nav-content" 
                onMouseEnter={() => setShowArtistsMegaMenu(true)}
                onMouseLeave={() => setShowArtistsMegaMenu(false)}
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
