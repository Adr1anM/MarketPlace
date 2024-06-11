import {Container, CssBaseline} from '@mui/material';
import {Author} from '../../types/types'
import { styled } from '@mui/material/styles';
import Grid from '@mui/material/Grid';
import Paper from '@mui/material/Paper';
import { dummyAuthors } from '../../dummyDataStore/artistsData';
import ArtistCard from '../cards/AtistCard';
import { useAuth } from '../../contexts/AuthContext';
import toast from 'react-hot-toast';

const ContentContainer = styled(Container)({
   margin: "2rem 0"
  });


const Item = styled(Paper)(({ theme }) => ({
  backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
  ...theme.typography.body2,
  padding: theme.spacing(1),
  textAlign: 'center',
  color: theme.palette.text.secondary,
}));



const ArtistsPage = () =>{
    const {isLoggedIn} = useAuth();

    if(!isLoggedIn){
        toast.error("You need to sign in");
        return null;
    }
    return(
        <>
            <CssBaseline />
            <Container maxWidth={false}
                       sx={{ bgcolor: 'gray',
                         width: '100%',
                         height: '100%',
                         border: 'InactiveCaption',
                         display: 'flex', 
                         flexDirection: 'column', 
                         alignItems: 'center',  
                         justifyContent: 'center', 
                       }}>
            <ContentContainer>
                <Grid container rowSpacing={2} columnSpacing={{ xs: 1, sm: 2, md: 3 }}>
                    {dummyAuthors.map((artist: Author) => (
                        <Grid item xs={12} sm={6} md={4} lg={3} key={artist.id}>
                            <Item>
                                <ArtistCard artist={artist}/>
                            </Item>
                        </Grid>
                    ))}     
                </Grid>
            </ContentContainer>
            </Container>
        </>
    );  
}


export default ArtistsPage;