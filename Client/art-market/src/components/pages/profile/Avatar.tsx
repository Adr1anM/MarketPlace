import { Fab, Paper } from "@mui/material";

import { memo } from "react";
import { Author } from "../../../types/types";
import { Avatar } from '@mui/material';   
import '../pagesStyles/ProfilePage.css';

interface AvatarProps {
    authorData: Author | null | undefined;
  }
  
  const ProfileAvatar = memo<AvatarProps>(({ authorData }) => {
    return (
      <>
        <Avatar className="avatarContainer" aria-label="recipe">
          <Paper className="avatarPaper" variant="outlined">
            {authorData?.profileImage ? (
              <img
                className="avatarImage"
                src={`data:image/jpeg;base64,${authorData?.profileImage?.toString()}`}
                alt="Avatar"
              />
            ) : (
              <img
                className="avatarImage"
                src={"https://upload.wikimedia.org/wikipedia/commons/a/a2/Person_Image_Placeholder.png"}
                alt="Placeholder"
              />
            )}
          </Paper>
        </Avatar>
        
      </>
    );
  });

  export default ProfileAvatar;