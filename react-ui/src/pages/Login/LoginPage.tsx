import { useState } from 'react';
import { Box, Button, TextField, Typography, Alert } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { LOGIN_MUTATION } from '../../graphql/loginQueries';
import { useMutation } from '@apollo/client';

const LoginPage = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();
  const [login, { loading }] = useMutation(LOGIN_MUTATION);

  
 const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setError('');

    try {
      const { data } = await login({
        variables: {
          request: {  
            email,    
            password  
          }
        }
      });

      // Store auth data
      localStorage.setItem('jwtToken', data.login.token);
      localStorage.setItem('tokenExpiry', data.login.expiry);
      localStorage.setItem('userRole', data.login.role);
      
      navigate('/');
    } catch (err) {
      setError('Login failed. Please try again');
      console.error('Login error:', err);
    }
  };


  return (
    <Box maxWidth={400} mx="auto" mt={10} p={3} boxShadow={3}>
      <Typography variant="h5" mb={3} textAlign="center">Login</Typography>
      {error && <Alert severity="error" sx={{ mb: 2 }}>{error}</Alert>}
      <form onSubmit={handleSubmit}>
        <TextField
          label="Username"
          fullWidth
          required
          margin="normal"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <TextField
          label="Password"
          type="password"
          fullWidth
          required
          margin="normal"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <Button type="submit" variant="contained" fullWidth sx={{ mt: 2 }} disabled={loading}> {loading ? 'Logging in...' : 'Login'}</Button>
      </form>
    </Box>
  );
};

export default LoginPage;
