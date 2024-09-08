/*import React, { useEffect } from 'react';
import Container from '@mui/material/Container';
import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import Paper from '@mui/material/Paper';
import { styled } from '@mui/material/styles';
import 'aos/dist/aos.css';
import AOS from 'aos';
//import /App.css;


const HeroContent = styled(Box)(({ theme }) => ({
    backgroundColor: '#123456',

    color: 'white',
    textAlign: 'center',
    width: '100%',
    minHeight: '60vh',
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: theme.spacing(2, 0),
    fontFamily: 'Poppins, sans-serif',  // Applying Poppins font
}));

const StyledCard = styled(Paper)(({ theme }) => ({
    padding: theme.spacing(4),
    textAlign: 'top',
    backgroundColor: '#ffffff',
    color: 'black',
    borderRadius: '15px',
    boxShadow: '0 4px 20px rgba(0, 0, 0, 0.1)',
    transition: 'transform 0.3s ease, box-shadow 0.3s ease',
    fontFamily: 'Poppins, sans-serif',  // Applying Poppins font
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
        <Box sx={{ width: '100%', pt: '7px' }}>
            <HeroContent
                style={{
                    backgroundImage: 'url(/src/assets/background.jpg)', // Path to your image
                    
                    backgroundSize: 'cover', // Cover the entire container
                    backgroundPosition: 'center', // Center the image
                    backgroundRepeat: 'no-repeat', // Prevent repeating
                    height: '75vh', // Full viewport height
                    margin: 0, // Remove default margin
                    //border: '20px solid #123', // Set the border
                }}>
                <Container
                    maxWidth="sm"
                    data-aos="fade-up"
                    className="containerZoomEffect"
                    style={{
                        backgroundColor: '#123',
                        padding: '15px',
                        paddingTop: '22px',
                    }} // Replace with your desired color
                >
                    <Typography variant="h1" gutterBottom>
                        Utilize Your Task
                        <span></span>
            <span></span>
            <span></span>
            <span></span>
                    </Typography>
                </Container>
                
            </HeroContent>
            
        </Box>
    );
};

export default Home;*/


import React, { useEffect } from 'react';
import Container from '@mui/material/Container';
import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';
import { styled } from '@mui/material/styles';
import 'aos/dist/aos.css';
import AOS from 'aos';

// Styling for the outer hero section
const HeroContent = styled(Box)(({ theme }) => ({
    backgroundColor: '#123456',
    color: 'white',
    textAlign: 'center',
    width: '100%',
    minHeight: '60vh',
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    padding: theme.spacing(2, 0),
    fontFamily: 'Poppins, sans-serif',  // Applying Poppins font
    overflow: 'hidden', // Ensure content doesn't overflow
}));

// Styling for the inner box with the neon border effect
const NeonBorderContainer = styled(Container)(({ theme }) => ({
    position: 'relative',
    padding: '15px',
    backgroundColor: '#123',
    paddingTop: '22px',
    zIndex: 1, // Ensure the neon border stays on top of background elements
    '&::before': {
        content: '""',
        position: 'absolute',
        top: 0,
        left: 0,
        width: '100%',
        height: '100%',
        border: '4px solid transparent',
        boxSizing: 'border-box',
        zIndex: 2,
        animation: 'neon-border 2s linear infinite',
    },
    '@keyframes neon-border': {
        '0%': {
            borderTop: '4px solid #00FFFF', // Neon cyan
            borderLeft: '4px solid transparent',
            borderRight: '4px solid transparent',
            borderBottom: '4px solid transparent',
        },
        
        
        '75%': {
            borderTop: '4px solid transparent',
            borderLeft: '4px solid transparent',
            borderRight: '4px solid transparent',
            borderBottom: '4px solid #00FFFF',
        },
        '100%': {
            borderTop: '4px solid #00FFFF',
            borderLeft: '4px solid transparent',
            borderRight: '4px solid transparent',
            borderBottom: '4px solid transparent',
        },
    },
}));

// Main component with the neon border effect
const Home = () => {
    useEffect(() => {
        AOS.init({
            duration: 2000,
            once: true,
        });
    }, []);

    return (
        <Box sx={{ width: '100%', pt: '7px' }}>
            <HeroContent
                style={{
                    backgroundImage: 'url(/src/assets/background.jpg)', // Path to your image
                    backgroundSize: 'cover', // Cover the entire container
                    backgroundPosition: 'center', // Center the image
                    backgroundRepeat: 'no-repeat', // Prevent repeating
                    height: '75vh', // Full viewport height
                    margin: 0, // Remove default margin
                }}
            >
                <NeonBorderContainer
                    maxWidth="sm"
                    data-aos="fade-up"
                >
                    <Typography variant="h1" gutterBottom sx={{ color: 'white' }}>
                        Manage Your Task
                    </Typography>
                </NeonBorderContainer>
            </HeroContent>
        </Box>
    );
};

export default Home;

