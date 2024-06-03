import { Avatar, Card, CardActions, CardContent, CardHeader, Collapse, IconButton, Typography } from '@mui/material';
import { red } from '@mui/material/colors';
import React from 'react';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';

interface ArtworkCardProps {
  artwork: {
    id: number;
    title: string;
    description: string;
    price: number;
    createdDate: string;
  };
}

const ArtworkCard: React.FC<ArtworkCardProps> = ({ artwork }) => {

  const [expanded, setExpanded] = React.useState(false);
  const handleExpandClick = () => {
    setExpanded(!expanded);
  };

  return (
    <Card  sx={{ maxWidth: 345 }}>
      <CardHeader id={artwork.id}
        avatar={
          <Avatar sx={{ bgcolor: red[500] }} aria-label="recipe">
            R
          </Avatar>
        }
        title={artwork.title}
        subheader={new Date(artwork.createdDate).toLocaleDateString()}
      />
      <CardContent>
        <Typography noWrap variant="body2" color="text.secondary">
          {artwork.description}
        </Typography>
      </CardContent>
      <CardActions disableSpacing>
        <IconButton aria-label="show more" onClick={handleExpandClick}>
          <ExpandMoreIcon />
        </IconButton>
      </CardActions>
      <Collapse in={expanded} timeout="auto" unmountOnExit>
        <CardContent>
          <Typography paragraph>Price: ${artwork.price}</Typography>
        </CardContent>
      </Collapse>
    </Card>
  );
};

export default ArtworkCard;
