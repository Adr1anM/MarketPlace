import React, { useState, useRef, useEffect, CSSProperties } from 'react';
import './navBar.css';
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
import LogInModal from './LogInModal';




const NavBar: React.FC = () => {
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);
  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };

  const [isDropdownVisible, setIsDropdownVisible] = useState(false);
  const dropdownRef = useRef<HTMLDivElement>(null);
  const buttonRef = useRef<HTMLButtonElement>(null);
  const timeoutRef = useRef<number | null>(null);

  const showDropdown = () => {
    if (timeoutRef.current) {
      clearTimeout(timeoutRef.current);
      timeoutRef.current = null;
    }
    setIsDropdownVisible(true);
  };

  const hideDropdown = () => {
    if (timeoutRef.current) {
      clearTimeout(timeoutRef.current);
    }
    timeoutRef.current = window.setTimeout(() => {
      setIsDropdownVisible(false);
    }, 200);
  };

  const handleMouseEnter = () => {
    showDropdown();
  };

  const handleMouseLeave = () => {
    hideDropdown();
  };

 

  useEffect(() => {
    const handleDocumentClick = (e: MouseEvent) => {
      if (
        dropdownRef.current &&
        !dropdownRef.current.contains(e.target as Node) &&
        buttonRef.current &&
        !buttonRef.current.contains(e.target as Node)
      ) {
        setIsDropdownVisible(false);
      }
    };

    document.addEventListener('click', handleDocumentClick);
    return () => {
      document.removeEventListener('click', handleDocumentClick);
    };
  }, []);


  const dropdownButtonStyle: CSSProperties  = {
    position: 'fixed',
    top: buttonRef.current?.getBoundingClientRect().bottom,
    left: 0,
    right: 0,
    width: '100vw',
    border: '1px solid #ccc',
    backgroundColor: '#fff',
    boxShadow: '0px 8px 16px rgba(0,0,0,0.2)',
    zIndex: 1,
    display: 'flex',
    justifyContent: 'center',
  };

  return (
    <>
      <div className="header-menu-wrapper-desktop">
        <header className="artshop-header">
          <div className="header-content">
            <div>
              <a className="header-title" href="/">ArtIs</a>
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
              <button
                ref={buttonRef}
                onMouseEnter={handleMouseEnter}
                onMouseLeave={handleMouseLeave}
                style={{ background: 'black' }}
              >
                <a href="" style={{ color: 'white' }}>Hover me</a>
              </button>
              {isDropdownVisible && (
                <div
                  ref={dropdownRef}
                  onMouseEnter={handleMouseEnter}
                  onMouseLeave={handleMouseLeave}
                  style={dropdownButtonStyle}
                >
                  <ul style={{ listStyleType: 'none', margin: 0, padding: '10px', color: 'black' }}>
                    <li>
                      <a href=""> category 1</a>
                    </li>
                    <li>Category 2</li>
                    <li>Category 3</li>
                    <li>Category 4</li>
                  </ul>
                </div>
              )}
              <div className="nav-content">
                <a href="/second-side">Artworks</a>
                <div className="dropdown">
                  <a href="/artworks/paintings">Paintings</a>
                  <a href="/artworks/sculptures">Sculptures</a>
                  <a href="/artworks/photography">Photography</a>
                </div>
              </div>
              <div className="nav-content"><a href="/collections">Collections</a></div>
              <div className="nav-content"><a href="/auctions">Auctions</a></div>
            </div>
            <div className="nav-container-section">
              <div className="nav-content"><a href="/buy-art">Buy Art</a></div>
              <div className="nav-content"><a href="/media">Media</a></div>
              <div className="nav-content"><a href="/seller">Seller</a></div>
              <div className="nav-content"><a href="/about-us">About Us</a></div>
            </div>
          </div>
        </nav>
      </div>
    </>
  );
};

export default NavBar;
