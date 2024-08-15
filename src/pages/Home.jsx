import React, { useEffect } from 'react';
import Container from '@mui/material/Container';
import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import Paper from '@mui/material/Paper';
import { styled } from '@mui/material/styles';
import 'aos/dist/aos.css';
import AOS from 'aos';


const HeroContent = styled(Box)(({ theme }) => ({
    backgroundColor: '#f5f5f5',
    color: 'black',
    textAlign: 'left', // Align text to the left
    width: '100%',
    minHeight: '60vh',
    display: 'flex',
    justifyContent: 'space-between', // Space between text and image
    alignItems: 'center',
    padding: theme.spacing(2, 0),
}));

const StyledCard = styled(Paper)(({ theme }) => ({
    padding: theme.spacing(4),
    textAlign: 'center',
    backgroundColor: '#ffffff',
    color: 'black',
    borderRadius: '15px',
    boxShadow: '0 4px 20px rgba(0, 0, 0, 0.1)',
    transition: 'transform 0.3s ease, box-shadow 0.3s ease',
    '&:hover': {
        transform: 'translateY(-10px)',
        boxShadow: '0 10px 25px rgba(0, 0, 0, 0.2)',
        backgroundColor: '#f0f0f0',
    },
}));

const Home = () => {
    useEffect(() => {
        AOS.init({
            duration: 2000,
            once: true,
        });
    }, []);

    return (
        <Box sx={{ width: '100%', pt: '64px' }}>
            <HeroContent>
                <Container maxWidth="sm" data-aos="fade-up">
                    <Typography variant="h1" gutterBottom>
                        Welcome to Wheel Factory
                    </Typography>
                    <Typography variant="h5">
                        Your one-stop solution for wheel remanufacturing.
                    </Typography>
                </Container>
                <Box component="img"
                    src="src/assets/Screenshot 2024-08-15 at 2.10.53 PM.png" // Replace with the correct path to your image
                    alt="Trucks"
                    sx={{
                        maxHeight: '300px', // Adjust the height as needed
                        marginLeft: 4,  // Space between text and image
                        objectFit: 'contain',
                    }}
                    data-aos="fade-left" />
            </HeroContent>
            <Container maxWidth="md" sx={{ mb: 8 }}>
                <Grid container spacing={4} justifyContent="center">
                    <Grid item xs={12} sm={6} md={4} data-aos="fade-right">
                        <StyledCard>
                            <Typography variant="h5" component="h2">
                                High Quality
                            </Typography>
                            <Typography>
                                We provide top-notch remanufacturing services ensuring high quality.
                            </Typography>
                        </StyledCard>
                    </Grid>
                    <Grid item xs={12} sm={6} md={4} data-aos="fade-up">
                        <StyledCard>
                            <Typography variant="h5" component="h2">
                                Expert Team
                            </Typography>
                            <Typography>
                                Our team consists of industry experts with years of experience.
                            </Typography>
                        </StyledCard>
                    </Grid>
                    <Grid item xs={12} sm={6} md={4} data-aos="fade-left">
                        <StyledCard>
                            <Typography variant="h5" component="h2">
                                Customer Support
                            </Typography>
                            <Typography>
                                We offer 24/7 customer support to assist you with any queries.
                            </Typography>
                        </StyledCard>
                    </Grid>
                </Grid>
            </Container>
        </Box>
    );
};

export default Home;
