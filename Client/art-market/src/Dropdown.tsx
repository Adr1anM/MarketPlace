import React, { useState, useRef, useEffect } from 'react';

const DropdownButton: React.FC = () => {
  const [isDropdownVisible, setIsDropdownVisible] = useState(false);
  const dropdownRef = useRef<HTMLDivElement>(null);
  const buttonRef = useRef<HTMLButtonElement>(null);

  const handleMouseEnter = () => {
    setIsDropdownVisible(true);
  };

  const handleMouseLeaveButton = (e: React.MouseEvent<HTMLButtonElement>) => {
    if (
      dropdownRef.current &&
      !dropdownRef.current.contains(e.relatedTarget as Node)
    ) {
      setIsDropdownVisible(false);
    }
  };

  const handleMouseLeaveDropdown = (e: React.MouseEvent<HTMLDivElement>) => {
    if (
      buttonRef.current &&
      !buttonRef.current.contains(e.relatedTarget as Node)
    ) {
      setIsDropdownVisible(false);
    }
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

  return (
    <div style={{ position: 'relative', display: 'inline-block' }}>
      <button
        ref={buttonRef}
        onMouseEnter={handleMouseEnter}
        onMouseLeave={handleMouseLeaveButton}
        style={{background: 'black'}}
      >
        <a href="" style={{color: 'white'}}>Hover me</a>
      </button>
      {isDropdownVisible && (
        <div
          ref={dropdownRef}
          onMouseEnter={handleMouseEnter}
          onMouseLeave={handleMouseLeaveDropdown}
          style={{
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
          }}
        >
          <ul style={{ listStyleType: 'none', margin: 0, padding: '10px' }}>
            <li>Category 1</li>
            <li>Category 2</li>
            <li>Category 3</li>
            <li>Category 4</li>
          </ul>
        </div>
      )}
    </div>
  );
};

export default DropdownButton;
