// utils/auth.ts
export const checkAuth = (): boolean => {
  const token = localStorage.getItem('jwtToken');
  const expiry = localStorage.getItem('tokenExpiry');
  
  if (!token || !expiry) return false;
  
  // Check if token is expired
  const now = new Date();
  const expiryDate = new Date(expiry);
  
  if (now > expiryDate) {
    // Token expired - clear storage
    localStorage.removeItem('jwtToken');
    localStorage.removeItem('tokenExpiry');
    localStorage.removeItem('userRole');
    return false;
  }
  
  return true;
};

export const logout = () => {
  localStorage.removeItem('jwtToken');
  localStorage.removeItem('tokenExpiry');
  localStorage.removeItem('userRole');
  window.location.href = '/login'; // Full page reload to reset state
};