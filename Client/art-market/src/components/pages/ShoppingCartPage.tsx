import React, { useEffect } from 'react';
import useShoppingCart from '../../zsm/stores/useShoppingCart'; // Adjust the import path as needed
import {
  Box,
  Typography,
  List,
  ListItem,
  ListItemText,
  IconButton,
  Button,
  Divider,
  Paper,
  Container,
  Grid,
  CircularProgress,
} from '@mui/material';
import { Add as AddIcon, Remove as RemoveIcon, Delete as DeleteIcon } from '@mui/icons-material';
import { AddToCartModel } from '../../types/types';
import { useAuth } from '../../contexts/AuthContext';

const ShoppingCartPage: React.FC = () => {
  const { 
    getCart, 
    getCartItems, 
    getCartItemCount, 
    removeFromCart, 
    updateCartItem, 
    fetchCart 
  } = useShoppingCart();


  const {user} = useAuth();
  const cart = getCart();
  const cartItems = getCartItems();
  const itemCount = getCartItemCount();

  useEffect(() => {
    const fetchAllCartItems = async () =>{
        await fetchCart(user?.id);
    }

    fetchAllCartItems();
  },[])

  const handleQuantityChange = (productId: number, newQuantity: number) => {
    if (newQuantity > 0) {
        const item:AddToCartModel ={
            userId: user?.id,
            productId: productId,
            quantity: newQuantity

        }
      updateCartItem(item);
    } else {
      removeFromCart(cart.userId, productId);
    }
  };

  const calculateTotal = () => {
    return cartItems.reduce((total, item) => total + item.price * item.quantity, 0);
  };

  if (cart.loading) {
    return (
      <Box display="flex" justifyContent="center" alignItems="center" height="100vh">
        <CircularProgress />
      </Box>
    );
  }

  return (
    <Container maxWidth="md">
      <Typography variant="h4" component="h1" gutterBottom>
        Your Shopping Cart
      </Typography>
      {itemCount === 0 ? (
        <Typography>Your cart is empty.</Typography>
      ) : (
        <Paper elevation={3}>
          <List>
            {cartItems.map((item, index) => (
              <React.Fragment key={item.productId}>
                <ListItem>
                  <Grid container spacing={2} alignItems="center">
                    <Grid item xs={12} sm={6}>
                      <ListItemText
                        primary={item.title}
                        secondary={item.productDescription}
                      />
                    </Grid>
                    <Grid item xs={12} sm={2}>
                      <Typography>${item.price.toFixed(2)}</Typography>
                    </Grid>
                    <Grid item xs={12} sm={2}>
                      <Box display="flex" alignItems="center">
                        <IconButton onClick={() => handleQuantityChange(item.productId, item.quantity - 1)}>
                          <RemoveIcon />
                        </IconButton>
                        <Typography>{item.quantity}</Typography>
                        <IconButton onClick={() => handleQuantityChange(item.productId, item.quantity + 1)}>
                          <AddIcon />
                        </IconButton>
                      </Box>
                    </Grid>
                    <Grid item xs={12} sm={2}>
                      <IconButton onClick={() => removeFromCart(cart.userId, item.productId)}>
                        <DeleteIcon />
                      </IconButton>
                    </Grid>
                  </Grid>
                </ListItem>
                {index < cartItems.length - 1 && <Divider />}
              </React.Fragment>
            ))}
          </List>
        </Paper>
      )}
      {itemCount > 0 && (
        <Box mt={3}>
          <Paper elevation={3}>
            <Box p={2}>
              <Typography variant="h6">Cart Summary</Typography>
              <Typography>Total Items: {itemCount}</Typography>
              <Typography>Total Price: ${calculateTotal().toFixed(2)}</Typography>
              <Box mt={2}>
                <Button variant="contained" color="primary" fullWidth>
                  Proceed to Checkout
                </Button>
              </Box>
            </Box>
          </Paper>
        </Box>
      )}
    </Container>
  );
};

export default ShoppingCartPage;