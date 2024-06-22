import * as React from 'react';
import { styled } from '@mui/material/styles';
import Button from '@mui/material/Button';
import CloudUploadIcon from '@mui/icons-material/CloudUpload';
import toast from 'react-hot-toast';

const VisuallyHiddenInput = styled('input')({
  clip: 'rect(0 0 0 0)',
  clipPath: 'inset(50%)',
  height: 1,
  overflow: 'hidden',
  position: 'absolute',
  bottom: 0,
  left: 0,
  whiteSpace: 'nowrap',
  width: 1,
  accept:"image/jpeg,image/png,image/gif,image/bmp,image/tiff,image/webp,image/svg+xml"
});

interface InputFileUploadProps {
  onUpload: (file: File) => void;
}

const InputFileUpload: React.FC<InputFileUploadProps> = ({ onUpload }) => {
  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const fileList = event.target.files;
    if (fileList && fileList.length > 0) {
      const file = fileList[0];
      if(file.type.startsWith("image/")){
        onUpload(file);
      }else{
        toast.error("Please upload a valid image file.");
      }
      
    }
  };

  return (
    <Button
      component="label"
      role={undefined}

      variant="contained"
      startIcon={<CloudUploadIcon />}
    >
      Upload file
      <VisuallyHiddenInput type="file" onChange={handleFileChange} />
    </Button>
  );
};

export default InputFileUpload;
