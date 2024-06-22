import React from 'react';
import { Box, Accordion, AccordionSummary, AccordionDetails, AccordionActions, Button, Divider } from '@mui/material';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';

interface DrawerListProps {
    toggleDrawer: (newOpen: boolean) => () => void;
}

const DrawerList: React.FC<DrawerListProps> = ({ toggleDrawer }) => {
    return (
        <Box sx={{ width: 250 }} role="presentation" onClick={(e) => e.stopPropagation()}>
            <Accordion defaultExpanded>
                <AccordionSummary expandIcon={<ExpandMoreIcon />} aria-controls="panel1-content" id="panel1-header">
                    Accordion 1
                </AccordionSummary>
                <AccordionDetails>
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse malesuada lacus ex, sit amet blandit leo lobortis eget.
                </AccordionDetails>
            </Accordion>
            <Accordion defaultExpanded>
                <AccordionSummary expandIcon={<ExpandMoreIcon />} aria-controls="panel2-content" id="panel2-header">
                    Accordion 2
                </AccordionSummary>
                <AccordionDetails>
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse malesuada lacus ex, sit amet blandit leo lobortis eget.
                </AccordionDetails>
            </Accordion>
            <Accordion defaultExpanded>
                <AccordionSummary expandIcon={<ExpandMoreIcon />} aria-controls="panel3-content" id="panel3-header">
                    Accordion Actions
                </AccordionSummary>
                <AccordionDetails>
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse malesuada lacus ex, sit amet blandit leo lobortis eget.
                </AccordionDetails>
                <AccordionActions>
                </AccordionActions> 
            </Accordion>
            <Divider />
            <Button onClick={toggleDrawer(false)}>Close Drawer</Button>
        </Box>
    );
};

export default DrawerList;
