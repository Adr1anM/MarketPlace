// MegaMenu.tsx
import React, { useEffect, useState } from 'react';
import './megaMenu.css';
import { Filter } from '../../../../types/types';
import { useNavigate } from 'react-router-dom';


export interface MegaMenuSection {
  title: string;
  links: { text: string; href: string; filterOptions: Filter }[];
}

interface MegaMenuProps {
  sections: MegaMenuSection[];
  className?: string;
  isOpen : boolean;
  handleArtworkLinkClick: (priceRange: string) => void;
}

const MegaMenu: React.FC<MegaMenuProps> = ({ sections, className = '', isOpen,handleArtworkLinkClick }) => {
  const [isVisible, setIsVisible] = useState(false);
  const [isActive, setIsActive] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    if(isOpen){
        setIsVisible(true);
        setTimeout(() => setIsActive(true), 50);
    }
    else{
        setIsActive(false);
        setTimeout(() => setIsVisible(false), 300);
    }
  },[isOpen]);

  if(!isVisible) return null;
    
  return (
    <div className={`megamenu-wrapper ${isActive ? 'active' : ''} ${className}`}>
      <div className="megamenu-background"></div>
      <div className="megamenu-container">
        <div className="megamenu">
          {sections.map((section, index) => (
            <div key={index} className="megamenu-section">
              <h3>{section.title}</h3>
              {section.links.map((link, linkIndex) => (
                <a key={linkIndex} href={link.href}  onClick={(e) => {
                  e.preventDefault();
                  if (link.filterOptions) {
                    const { path, value, operator } = link.filterOptions;
                    const queryParams = new URLSearchParams({ path, value, operator }).toString();
                    navigate(`/artworks?${queryParams}`);
                  }
                }} >
                  {link.text}
                </a>    
              ))}
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default MegaMenu;