import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Container from '@mui/material/Container';
import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import Grid from '@mui/material/Grid';
import { styled } from '@mui/material/styles';
import axios from 'axios';

const validateUserUrl = import.meta.env.VITE_APP_USER_VALIDATE;


const GradientBackground = styled(Box)(({ theme }) => ({
    background: 'linear-gradient(135deg, #12345 0%, #f5f5f5 100%)', // Soft grey gradient
    minHeight: '80vh',
    paddingBottom: '20px',
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
}));

/*const GlassmorphismContainer = styled(Box)(({ theme }) => ({
    padding: theme.spacing(6),
    borderRadius: '20px',
    background: '#123',
    boxShadow: '0 4px 30px rgba(0, 0, 0, 0.1)',
    backdropFilter: 'blur(10px)',
    border: '1px solid rgba(255, 255, 255, 0.3)',
    textAlign: 'center',
}));*/

const GlassmorphismContainer = styled(Box)(({ theme }) => ({
    padding: theme.spacing(6),
    paddingTop: '20px',
    borderRadius: '20px',
    background: '#123',
    boxShadow: '0 4px 30px rgba(0, 0, 0, 0.1)',
    backdropFilter: 'blur(10px)',
    border: '1px solid rgba(255, 255, 255, 0.3)',
    textAlign: 'center',
    position: 'relative', // Make it positionable
    transition: 'all 0.5s ease', // Smooth transition for size and positioning
    zIndex: 1, // Ensure the box stays on top
    '&:hover': {
        position: 'absolute', // Position absolutely when hovered
        top: 0,
        left: 0,
        width: '100vw', // Expand to full width of the viewport
        height: '60vh', // Expand to full height of the viewport
        borderRadius: '0', // Remove border-radius to cover the page
        transform: 'none', // Reset any scaling
        //zIndex: 9999, // Ensure it stays above other elements
        boxShadow: '0 8px 40px rgba(0, 0, 0, 0.2)', // Enhance shadow
    },
}));







const StyledTextField = styled(TextField)(({ theme }) => ({
    '& .MuiInputBase-input': {
        color: 'white',
    },
    '& .MuiInputLabel-root': {
        color: 'rgba(0, 0, 0, 0.6)',
    },
    '& .MuiOutlinedInput-root': {
        '& fieldset': {
            borderColor: 'rgba(0, 0, 0, 0.3)',
        },
        '&:hover fieldset': {
            borderColor: 'white',
        },
        '&.Mui-focused fieldset': {
            borderColor: 'white',
        },
    },
}));

const CustomButton = styled(Button)(({ theme }) => ({
    marginTop: theme.spacing(3),
    padding: theme.spacing(1.5),
    borderRadius: '50px',
    background: 'linear-gradient(135deg, #456, #9e9e9e)',  // Grey gradient button
    color: 'white',
    fontWeight: 'bold',
    textTransform: 'uppercase',
    letterSpacing: '1px',
    '&:hover': {
        background: 'linear-gradient(135deg, #9e9e9e, #456)',  // Inverted gradient on hover
    },
}));

const Login = () => {


    const [formData, setFormData] = useState({
        username: '',
        password: '',
    });
    const navigate = useNavigate();

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            console.log(validateUserUrl); // Log the URL to ensure it's correct

            const response = await axios.post(validateUserUrl, {
                username: formData.username,
                password: formData.password,
            });
            console.log(response.data);
            localStorage.setItem('token', response.data.token); // Store the token
            navigate('/orders'); // Redirect to orders page after successful login
        } catch (error) {
            console.error(error);
            alert('Invalid credentials');
        }
    };

    return (
        <GradientBackground>
            <Container component="main" maxWidth="xs">
                <GlassmorphismContainer>
                    <Typography component="h1" variant="h4" sx={{ color: 'white', fontWeight: 'bold', mb: 2 }}>
                        Task Manager
                    </Typography>
                    <Box component="form" noValidate onSubmit={handleSubmit} sx={{ mt: 3 }}>
                        <Grid container spacing={2}>
                            <Grid item xs={12}>
                                <StyledTextField
                                    variant="outlined"
                                    required
                                    fullWidth
                                    id="username"
                                    label="EmployeeId"
                                    name="username"
                                    autoComplete="username"
                                    value={formData.username}
                                    onChange={handleChange}
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <StyledTextField
                                    variant="outlined"
                                    required
                                    fullWidth
                                    name="password"
                                    label="Password"
                                    type="password"
                                    id="password"
                                    autoComplete="current-password"
                                    value={formData.password}
                                    onChange={handleChange}
                                />
                            </Grid>
                        </Grid>
                        <CustomButton
                            type="submit"
                            fullWidth
                        >
                            Login
                        </CustomButton>
                    </Box>
                </GlassmorphismContainer>
            </Container>
        </GradientBackground>
    );
};

export default Login;
