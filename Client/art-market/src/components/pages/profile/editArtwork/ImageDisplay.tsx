import { Box } from "@mui/material";
import { Artwork } from "../../../../types/types";


const ImageDisplay = ({ artwork }: { artwork: Artwork }) => (
    <Box
      component="img"
      sx={{
        height: 200,
        width: 200,
        objectFit: "cover",
      }}
      alt="Artwork"
      src={`data:image/jpeg;base64,${artwork?.imageData?.toString()}`}
    />
  );


export default ImageDisplay;