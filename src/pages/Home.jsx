// src/pages/Home.jsx
import React from 'react';
import Container from '@mui/material/Container';
import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import Paper from '@mui/material/Paper';
import { styled } from '@mui/material/styles';

const HeroContent = styled(Box)(({ theme }) => ({
    backgroundColor: '#f5f5f5', // Light gray background
    color: 'black',
    textAlign: 'center',
    width: '100%',
    minHeight: '60vh',
    display: 'flex',
    flexDirection: 'column',
    justifyContent: 'center',
    alignItems: 'center',
    padding: theme.spacing(2, 0),
}));

const Card = styled(Paper)(({ theme }) => ({
    padding: theme.spacing(4),
    textAlign: 'center',
    backgroundColor: '#ffffff', // Pure white background for contrast
    color: 'black',
    boxShadow: 'none', // Removing shadow for a flat design
    '&:hover': {
        backgroundColor: '#eeeeee', // Light hover effect
    },
}));

const Home = () => {
    return (
        <Box sx={{ width: '100%', pt: '64px' }}>
            <HeroContent>
                <Container maxWidth="sm">
                    <Typography variant="h1" gutterBottom>
                        Welcome to Wheel Factory
                    </Typography>
                    <Typography variant="h5">
                        Your one-stop solution for wheel remanufacturing.
                    </Typography>
                </Container>
            </HeroContent>
            <Container maxWidth="md" sx={{ mb: 8 }}>
                <Grid container spacing={4} justifyContent="center">
                    <Grid item xs={12} sm={6} md={4}>
                        <Card>
                            <Typography variant="h5" component="h2">
                                High Quality
                            </Typography>
                            <Typography>
                                We provide top-notch remanufacturing services ensuring high quality.
                            </Typography>
                        </Card>
                    </Grid>
                    <Grid item xs={12} sm={6} md={4}>
                        <Card>
                            <Typography variant="h5" component="h2">
                                Expert Team
                            </Typography>
                            <Typography>
                                Our team consists of industry experts with years of experience.
                            </Typography>
                        </Card>
                    </Grid>
                    <Grid item xs={12} sm={6} md={4}>
                        <Card>
                            <Typography variant="h5" component="h2">
                                Customer Support
                            </Typography>
                            <Typography>
                                We offer 24/7 customer support to assist you with any queries.
                            </Typography>
                        </Card>
                    </Grid>
                </Grid>
            </Container>
        </Box>
    );
};

export default Home;
