import { SxProps } from "@mui/material";


export const styles: { [key: string]: SxProps } = {
    card : {
        marginBottom: '32px',
        position: 'relative',
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        marginTop: '-64px',
        borderRadius: '0.75rem',
        height: '100%',
        flexDirection: 'column',
        width: '97%',
        overflow: 'visible',
    },

    container: {
        alignContent: 'center',
        bgcolor: 'white',
        color: 'white',
        width: '100%',
        height: '100%',
    }, 

    cardContainer:{
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
    },
    cardHeader:{
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        marginTop: '-64px',
    },

    avatar: {
        width: 110,
        height: 110,
    },

    avatarPaper : {
        width: '100%',
        height: '100%',
    },


    contentBox : {
        height: '300px',
    }
}