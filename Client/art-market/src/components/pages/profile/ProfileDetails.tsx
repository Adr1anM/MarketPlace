import { Box, Button, Grid, TextField, Typography } from "@mui/material";
import { Author, User } from "../../../types/types";
import { memo } from "react";
import '../pagesStyles/ProfilePage.css';
import useArtworks from "../../../zsm/stores/useArtworks";

interface ProfileDetailsProps {
    authorData: Author | null | undefined;
    isAuthorProfile: boolean | undefined;
    user: User | null | undefined;
    handleBiographyChange: (e: React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>) => void;
    editBio: boolean;
    handleSave: () => void;
  }
  
const ProfileDetails = memo<ProfileDetailsProps>(({ authorData, isAuthorProfile, user, handleBiographyChange, editBio, handleSave }) => {
    
    const artworksStore = useArtworks();

    return (

        <Box sx={{
            width: '90%', 
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            paddingTop: '80px'
            }}
        >
        
            <Box 
                component= "div"
                sx={{ 
                width: '60%',
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                flexDirection: 'column',
            
                }}
            >
                <Grid container  sx={{padding: '5px', width: '100%', height: '100%'}}>

                {isAuthorProfile ?(
                <Grid item xs={6}  md={12}>
                    <Typography className="grid-overall-num" align="left" variant="h4"  >
                    {user?.firstName + " " + user?.lastName}
                    </Typography>
                </Grid>
                ):(
                    <Grid container>
                    <Grid item xs={6}  md={9}>
                        <Typography className="grid-overall-num" align="left" variant="h4"  >
                        {user?.firstName + " " + user?.lastName}
                        </Typography>
                    </Grid>
                    <Grid item xs={6} md={3}>
                        <Button style={{outline: 'none', borderRadius: 15}} variant="outlined">FOLLOW</Button>
                    </Grid>
                </Grid>
                )}
                            
                <Grid container sx={{ paddingTop: '15px'}} rowSpacing={1} columnSpacing={1}>
                    <Grid item xs={6}  md="auto" >
                    <Typography className="grid-overall-num" component={'span'} align="center"  >
                        {artworksStore.artworks.length + " "}
                    </Typography>
                    <Typography className="bio-paragraph" component={'span'} align="center"  >
                        Posts
                    </Typography>
                    </Grid>
                    <Grid item xs={6}  md="auto">
                    <Typography className="grid-overall-num" component={'span'} align="center"  >
                        {200 + " "}  
                    </Typography>
                    <Typography className="bio-paragraph" component={'span'} align="center"  >
                        Followers
                    </Typography>
                    </Grid>
                    <Grid item xs={6}  md="auto">
                    <Typography className="grid-overall-num" component={'span'} align="center"  >
                        {300 + " "} 
                    </Typography>
                    <Typography className="bio-paragraph" component={'span'} align="center"  >
                    Following
                    </Typography>
                    </Grid>
                </Grid>
                {isAuthorProfile ? (
                    <TextField
                    value= {authorData?.biography}
                    multiline
                    sx={{width: '100%'}}
                    onChange={handleBiographyChange}
                    variant="outlined"
                    />
                ) :(
                    <Typography variant="h6" className="bio-paragraph">
                    {authorData?.biography}
                    </Typography>
                )}
            
                </Grid>
                { isAuthorProfile && editBio &&
                <Button sx={{'&:focus': {outline: 'none'}}} variant="contained" color="success" onClick={handleSave}>
                    Update
                </Button>
                }
            </Box> 
        </Box>
    );
});

export default ProfileDetails;