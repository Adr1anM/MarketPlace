import { Box, Button, Divider, IconButton, Modal, Stack } from "@mui/material";
import { FC, useEffect, useState } from "react";
import { Author } from "../../../types/types";
import InputFileUpload from "./InputFileUpload";
import CloseIcon from '@mui/icons-material/Close';
import '../pagesStyles/ProfilePage.css';

interface AvatarModalProps {
    open: boolean;
    onClose: () => void;
    author: Author | null | undefined;
    onDelete: () => void;
    onUpload: (file: File) => void; 
    onSave: () => Promise<void>;
  }

const style = {
    position: 'absolute' as 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 500,
    height: 400,
    bgcolor: 'background.paper',
    border: '2px solid #000',
    boxShadow: 24,
    p: 4,
    outline: 'none',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
};

const AvatarModal:FC<AvatarModalProps> = ({ open, onClose, author, onDelete, onUpload, onSave }) => {

  const [isImageModified, setIsImageModified] = useState(false);
  
  useEffect(() => {
    if (open) {
      setIsImageModified(false);
    }
  }, [open]);

  const handleDelete = () => {
    onDelete();
    setIsImageModified(true);
  };

  const handleUpload = (file: File) => {
    onUpload(file);
    setIsImageModified(true);
  };

  const handleSave = () =>{
    onSave();
    onClose();
  }

  return ( 
    <Modal open={open} onClose={onClose}>
      <Box sx={style}>
        <IconButton
            aria-label="close"
            onClick={() => {onClose()}} 
            sx={{
                position: 'absolute',
                right: 8,
                top: 8,
                color: (theme) => theme.palette.grey[500],
                outline: 'none',
            }}
        >
        <CloseIcon />
        </IconButton>
            {author?.profileImage ? (
              <Box
                component="img"
                sx={{
                    height: 200,
                    width: 200,
                    borderRadius: '50%',
                    objectFit: 'cover'
                }}
                alt="Author profile"
                src={`data:image/jpeg;base64,${author?.profileImage?.toString()}`}
              />
            ) : (
              <Box
                  component="img"
                  sx={{
                      height: 200,
                      width: 200,
                      borderRadius: '50%',
                      objectFit: 'cover'
                  }}
                  alt="Default profile placeholder"
                  src={"https://upload.wikimedia.org/wikipedia/commons/a/a2/Person_Image_Placeholder.png"}
              />
            )}
        <Stack sx={{marginTop: '15px'}} direction="row" gap="10px">
          <Button onClick={handleDelete}>Delete</Button>
        <InputFileUpload onUpload={handleUpload} />
        </Stack>  

        {isImageModified && (
          <Button sx={{marginTop: '15px'}} variant="contained" color="primary" onClick={handleSave}>
            Save
          </Button>
        )}
      </Box>
    </Modal>
  );

   
};


export default AvatarModal;  