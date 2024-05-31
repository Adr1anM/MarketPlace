import React from 'react';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';

const Content: React.FC = () => {
  return (
    <Box sx={{ p: 3 }}>
      <Typography variant="h4">
        Main Content
      </Typography>
      <Typography variant="body1">
        This is the main content area.
      </Typography>
    </Box>
  );
};

export default Content;
