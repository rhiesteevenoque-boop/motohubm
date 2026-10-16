/* MotoHub REST API helper. Load this file before page scripts when migrating a page from demo storage to the API. */
window.MotoHubAPI = (() => {
  const BASE_URL = 'http://localhost:5000/api';
  const tokenKey = 'motohub_api_token';
  async function request(path, options = {}) {
    const token = sessionStorage.getItem(tokenKey);
    const headers = { 'Content-Type': 'application/json', ...(options.headers || {}) };
    if (token) headers.Authorization = `Bearer ${token}`;
    const response = await fetch(`${BASE_URL}${path}`, { ...options, headers });
    if (response.status === 204) return null;
    const data = await response.json().catch(() => ({}));
    if (!response.ok) throw new Error(data.message || data.title || `Request failed (${response.status})`);
    return data;
  }
  return {
    async register(fullName, email, password) { return request('/auth/register', { method:'POST', body:JSON.stringify({fullName,email,password}) }); },
    async login(email, password) { const result = await request('/auth/login', { method:'POST', body:JSON.stringify({email,password}) }); sessionStorage.setItem(tokenKey,result.token); sessionStorage.setItem('motohub_api_user',JSON.stringify(result.user)); return result.user; },
    logout() { sessionStorage.removeItem(tokenKey); sessionStorage.removeItem('motohub_api_user'); },
    services() { return request('/services'); }, parts() { return request('/parts'); },
    motorcycles() { return request('/motorcycles'); }, addMotorcycle(data) { return request('/motorcycles',{method:'POST',body:JSON.stringify(data)}); },
    bookings() { return request('/bookings'); }, createBooking(data) { return request('/bookings',{method:'POST',body:JSON.stringify(data)}); },
    weather(latitude=8.35,longitude=124.87) { return request(`/weather?latitude=${encodeURIComponent(latitude)}&longitude=${encodeURIComponent(longitude)}`); }
  };
})();
