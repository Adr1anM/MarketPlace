import { Container } from "@mui/material";
import { styled } from "@mui/material/styles";

export const ContentContainer = styled(Container)({
    margin: "2rem 0"
});

export const buttonStyle = {
    color: 'black',
    outline: 'none',
    backgroundColor: 'white',
    boxShadow: '0px 4px 6px rgba(0, 0, 0, 0.1)',
    transition: 'box-shadow 0.3s ease-in-out',
    '&:focus': {
        outline: 'none',
    },
};