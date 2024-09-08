// src/components/Navbar.jsx
import React from 'react';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import { Link } from 'react-router-dom';
import { styled } from '@mui/material/styles';

const Offset = styled('div')(({ theme }) => theme.mixins.toolbar);

const StyledLink = styled(Link)(({ theme }) => ({
    color: 'black',
    margin: theme.spacing(0, 2),
    textDecoration: 'none',
    fontSize: '1rem',
    fontWeight: 'bold',
    fontFamily: 'Roboto, sans-serif',  // Applying Roboto font
    '&:hover': {
        textDecoration: 'underline',
    }
}));

const NavbarContainer = styled('div')(({ theme }) => ({
    display: 'flex',
    justifyContent: 'right',
    flexGrow: 1,
    fontFamily: 'Roboto, sans-serif',  // Ensuring consistent font application
}));

const Navbar = () => {
    return (
        <React.Fragment>
            <AppBar position="fixed" sx={{ backgroundColor: '#123', boxShadow: 'none' }}>
                <Toolbar sx={{ justifyContent: 'space-between' }}>
                    <Typography variant="h6" component={Link} to="/" sx={{
                        color: 'white',
                        textDecoration: 'none',
                        fontSize: '1.25rem',
                        fontFamily: 'Roboto, sans-serif'  // Consistent font application
                    }}>
                        Task Manager
                    </Typography>
                    <NavbarContainer>

                        <StyledLink to="/login" style={{
                            backgroundColor: '#fff',
                            padding: '7px',
                            borderRadius: '8px',
                            overflow: 'hidden',
                        }}>Login</StyledLink>
                        <StyledLink to="/orders" style={{
                            backgroundColor: '#fff',
                            padding: '7px',
                            borderRadius: '8px',
                            overflow: 'hidden',
                        }}>Assign Task</StyledLink>
                        <StyledLink to="/tasks" style={{
                            backgroundColor: '#fff',
                            padding: '7px',
                            borderRadius: '8px',
                            overflow: 'hidden',
                        }}>Complete Task</StyledLink>
                    </NavbarContainer>
                </Toolbar>
            </AppBar>
            <Offset />
        </React.Fragment>
    );
};

export default Navbar;
