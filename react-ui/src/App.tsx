import {
  AppBar,
  Box,
  IconButton,
  Toolbar,
  Typography,
  useTheme
} from '@mui/material';
import { Brightness4, Brightness7 } from '@mui/icons-material';
import { useContext, useEffect, useState, type JSX } from 'react';
import { ColorModeContext } from './theme/ColorModeContext';
import NavDrawer from './components/NavDrawer';
import { Routes, Route, useLocation, Navigate, useNavigate } from 'react-router-dom';
import Dashboard from './pages/Dashboard';
import Employee from './pages/Employee';
import CompanyPage from './pages/Company/CompanyPage';
import LoginPage from './pages/Login/LoginPage';
import { checkAuth, logout } from './utils/auth';

const drawerWidth = 240;

const ProtectedRoute = ({ children }: { children: JSX.Element }) => {
  // const token = localStorage.getItem('jwtToken');
  const location = useLocation();
  const isAuthenticated = checkAuth();

  if (!isAuthenticated) {
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  return children;
};


const App = () => {
  const theme = useTheme();
  const colorMode = useContext(ColorModeContext);
  const navigate = useNavigate();
  const isLoginPage = location.pathname === '/login';
  const [isAuthenticated, setIsAuthenticated] = useState(checkAuth());

  useEffect(() => {
    const authStatus = checkAuth();
    setIsAuthenticated(authStatus);

    if (authStatus && location.pathname === '/login') {
      navigate('/dashboard', { replace: true });
    } else if (!authStatus && location.pathname !== '/login') {
      navigate('/login', { replace: true });
    }

    const interval = setInterval(() => {
      const currentAuthStatus = checkAuth();
      setIsAuthenticated(currentAuthStatus);

      if (!currentAuthStatus && isAuthenticated) {
        logout();
      }
    }, 60000);

    return () => clearInterval(interval);
  }, [location.pathname, navigate, isAuthenticated]);

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  return (
    <Box sx={{ display: 'flex' }}>

      {location.pathname !== '/login' && (
        <>
          <AppBar position="fixed" sx={{ zIndex: (theme) => theme.zIndex.drawer + 1 }}>
            <Toolbar sx={{ justifyContent: 'space-between' }}>
              <Typography variant="h6" noWrap component="div">
                React MUI CRUD App
              </Typography>
              <Box display="flex" alignItems="center" gap={2}>
                <IconButton color="inherit" onClick={colorMode.toggleColorMode}>
                  {theme.palette.mode === 'dark' ? <Brightness7 /> : <Brightness4 />}
                </IconButton>
                <IconButton color="inherit" onClick={() => {
                  localStorage.removeItem('jwtToken');
                  navigate('/login');
                }}>
                  <Typography variant="body2" color="inherit">Logout</Typography>
                </IconButton>
              </Box>
            </Toolbar>
          </AppBar>
          <NavDrawer drawerWidth={drawerWidth} />
        </>
      )}

      <Box component="main" sx={{ flexGrow: 1, bgcolor: 'background.default', p: 3 }}>
        {location.pathname !== '/login' && <Toolbar />}
        <Routes>
          <Route path="/login" element={<LoginPage />} />
          <Route
            path="/"
            element={
              <ProtectedRoute>
                <Dashboard />
              </ProtectedRoute>
            }
          />
          <Route
            path="/company"
            element={
              <ProtectedRoute>
                <CompanyPage />
              </ProtectedRoute>
            }
          />
          <Route
            path="/employee"
            element={
              <ProtectedRoute>
                <Employee />
              </ProtectedRoute>
            }
          />
          <Route path="*" element={<Navigate to="/" />} />
        </Routes>
      </Box>
    </Box>
  );
};

export default App;
