import {
  AppBar,
  Box,
  IconButton,
  Toolbar,
  Typography,
  useTheme
} from '@mui/material';
import { Brightness4, Brightness7 } from '@mui/icons-material';
import { useContext } from 'react';
import { ColorModeContext } from './theme/ColorModeContext';
import NavDrawer from './components/NavDrawer';
import { Routes, Route } from 'react-router-dom';
import Dashboard from './pages/Dashboard';
import Employee from './pages/Employee';
import CompanyPage from './pages/Company/CompanyPage';

const drawerWidth = 240;

const App = () => {
  const theme = useTheme();
  const colorMode = useContext(ColorModeContext);

  return (
    <Box sx={{ display: 'flex' }}>
      <AppBar position="fixed" sx={{ zIndex: (theme) => theme.zIndex.drawer + 1 }}>
        <Toolbar sx={{ justifyContent: 'space-between' }}>
          <Typography variant="h6" noWrap component="div">
            React MUI CRUD App
          </Typography>
          <IconButton color="inherit" onClick={colorMode.toggleColorMode}>
            {theme.palette.mode === 'dark' ? <Brightness7 /> : <Brightness4 />}
          </IconButton>
        </Toolbar>
      </AppBar>

      <NavDrawer drawerWidth={drawerWidth} />

      <Box component="main" sx={{ flexGrow: 1, bgcolor: 'background.default', p: 3 }}>
        <Toolbar />
        <Routes>
          <Route path="/" element={<Dashboard />} />
          <Route path="/company" element={<CompanyPage />} />
          <Route path="/employee" element={<Employee />} />
        </Routes>
      </Box>
    </Box>
  );
};

export default App;
