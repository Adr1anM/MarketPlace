import React, { useEffect, useState } from 'react';
import {Box, Card, CardHeader,Fab,} from '@mui/material';
import AddIcon from '@mui/icons-material/Add';
import './pagesStyles/ProfilePage.css';
import { Author, CreateArtwork } from "../../types/types";
import useArtworks from "../../zsm/stores/useArtworks";
import { useAuth } from "../../contexts/AuthContext";
import useAuthors from "../../zsm/stores/useAuthors";
import { useParams } from "react-router-dom";
import toast from 'react-hot-toast';
import AvatarModal from './profile/EditPhotoModal';
import ProfileDetails from './profile/ProfileDetails';
import ArtworkList from './profile/ArtworkList';
import ProfileAvatar from './profile/Avatar';
import EditIcon from '@mui/icons-material/Edit';
import CreateArtworkModal from './profile/CreateArtworkModal';
import useCategories from '../../zsm/stores/useCategory';

const ProfilePage = () => {

  const {isLoggedIn,user} = useAuth();
  const { authorId } = useParams();
  const artworksStore = useArtworks();
  const authorStore = useAuthors(); 
  const categoryStore = useCategories();

  const isAuthorProfile = user?.roles.includes("Author") && !authorId && isLoggedIn;


  const [authorData, setAuthorData] = useState<Author | null>();
  const [originalAuthorData, setOriginalAuthorData] = useState<Author | null>();
  const [editImageModal, setEditImageModal] = useState(false);
  const [editBio, setEditBio] = useState(false);
  const [createArtworkModal, setCreateArtworkModal] = useState(false);

  console.log("user",user);
  console.log("author id from params",authorId);
  console.log("isAuthorProfile",isAuthorProfile);

  useEffect(() => {
    const fetchAuthorData = async () => {
      try {
        if (isAuthorProfile) {
          if (user?.id) {  

            const auth = await authorStore.fetchAuthorByUserId(user.id);
            console.log("id", user.id);
            console.log("Author", auth);
            setAuthorData(auth);
            setOriginalAuthorData(auth);
          }
        } else if (authorId) {
          const parsedAuthorId = parseInt(authorId);
          if (!isNaN(parsedAuthorId)) { 
            const auth = await authorStore.fetchAuthorById(parsedAuthorId);
            console.log("Author", auth);
            setAuthorData(auth);
            setOriginalAuthorData(auth);
          }
        }
      } catch (error) {
        console.error('Error fetching Author data:', error);
      }
    };

    fetchAuthorData();
  }, [isAuthorProfile, user, authorId,isLoggedIn]);


  useEffect(() => { 
    const fetchArtworks = async () => {
      if (authorData) {
        await artworksStore.fetchArtworksByAuthorId(authorData?.id);
      }
    };

    fetchArtworks();
  }, [authorData,isLoggedIn]);

                                                                                           
  useEffect(() => {
    const fetchCategories = async () =>{
      await categoryStore.fetchCategories();
    };
    fetchCategories()
  },[]);

  console.log("categories", categoryStore.categories);
  useEffect(() => {
    const artwork = artworksStore.getArtworkById(8);
    console.log("Artwork",artwork);
  },[])

  const handleOpenEditImageModal = () => {
    setEditImageModal(true);
  };

  const handleCloseEditImageModal = () => {
    setEditImageModal(false);
  };

  const handleDeleteImage = () =>{
    setAuthorData((prevAuthorData) => ({
      ...prevAuthorData!,
      profileImage: null, 
    }));
  };

  
  const handleUploadAvatar = (file: File) => {
    try {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => {
        const base64String = reader.result as string;
        const updatedAuthor = { 
          ...authorData!, 
          profileImage: base64String.replace(/^data:image\/[a-z]+;base64,/, "")
        };
        setAuthorData(updatedAuthor);
      };
    } catch (error) {
      console.error('Error uploading avatar:', error);
    }
  };

  const handleSave = async () => {

    setEditBio(false);
    try {
      if (authorData && originalAuthorData && JSON.stringify(authorData) !== JSON.stringify(originalAuthorData)) {
        await authorStore.updateAuthor(authorData);
        console.log('Author data saved successfully');
        toast.success('Author data saved successfully');
        setOriginalAuthorData(authorData);
        
      } else {
        console.log('No changes to save');
        toast('No changes to save');
      }
    } catch (error) {
      console.error('Error updating biography:', error);
      toast.error('Failed to save author data');
    }
  };


  const handleSaveArtwork = async (artwork: CreateArtwork) => {
    try {
     
        await artworksStore.createArtwork(artwork);
        console.log('Artwork created successfully');
        toast.success('Artwork created successfully');
        setOriginalAuthorData(authorData);
    } catch (error) {
      console.error('Error creating artwork:', error);
      toast.error('Failed to create Artwork');
    }
  };
  
  const handleDelete = async (id:number) =>{
    try {
     
      await artworksStore.deleteArtworkById(id);
      console.log('Artwork deleted successfully');
      toast.success('Artwork deleted successfully');
      setOriginalAuthorData(authorData);
  } catch (error) {
    console.error('Error deleteing biography:', error);
    toast.error('Failed to delete Artwork');
  }
  }

  const handleBiographyChange = (e: React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>) => {
    setEditBio(true);
    setAuthorData((prevAuthor) => ({
      ...prevAuthor!,
      biography: e.target.value,
    }));
  };

  return (
    <Box className="container">
      <Box className="css-t7o3hl">
      </Box>
      <Box className="cardContainer">
        <Card className="card" elevation={24}>
          <CardHeader
            className="cardHeader"
            avatar={<ProfileAvatar authorData={authorData}/>}
          />
          {isAuthorProfile && (
          <Fab
            sx={{
              '&:focus': { outline: 'none' },
              top: '-30px',
              left: '30px'
            }}
            size="small"
            color="secondary"
            aria-label="edit"
            onClick={handleOpenEditImageModal}
          >
            <EditIcon />
          </Fab>
        )}    
          <ProfileDetails
              authorData={authorData}
              isAuthorProfile={isAuthorProfile}            
              handleBiographyChange={handleBiographyChange}
              editBio={editBio}
              handleSave={handleSave}
          />
          <ArtworkList artworks={Array.isArray(artworksStore.artworks) ? artworksStore.artworks : []}  handleDelete={handleDelete} user={user} />
          <br />
        </Card>
      </Box>
      <Fab onClick={() => setCreateArtworkModal(true)} color="secondary" aria-label="edit" sx={{'&:focus': {outline: 'none'}, position: 'fixed', bottom: 30, right: 30 }}>
        <AddIcon  />
      </Fab>

    <AvatarModal author={authorData} open = {editImageModal} onClose={handleCloseEditImageModal} onDelete={handleDeleteImage} onSave={handleSave}  onUpload={handleUploadAvatar}/>
    <CreateArtworkModal open={createArtworkModal} onClose={() => setCreateArtworkModal(false)} onCreate={handleSaveArtwork} authorID={authorData?.id!} />
    </Box>
    
  );
}

export default ProfilePage;



