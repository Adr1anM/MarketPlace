// MegaMenu.tsx
import React, { useEffect, useState } from 'react';
import './megaMenu.css';
import { Flag } from '@mui/icons-material';

export interface MegaMenuSection {
  title: string;
  links: { text: string; href: string }[];
}

interface MegaMenuProps {
  sections: MegaMenuSection[];
  className?: string;
  isOpen : boolean;
}

const MegaMenu: React.FC<MegaMenuProps> = ({ sections, className = '', isOpen }) => {
  const [isVisible, setIsVisible] = useState(false);
  const [isActive, setIsActive] = useState(false);

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
                <a key={linkIndex} href={link.href}>
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