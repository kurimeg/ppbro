import axios from 'axios'

const client = axios.create({
  baseURL: 'https://bchk-api.azurewebsites.net/api/'
})

client.interceptors.request.use((config) => {
  return config
}, (error) => {
  return Promise.reject(error)
})

client.interceptors.response.use((response) => {
  return Promise.resolve(response.data)
}, (error) => {
  return Promise.reject(error)
})
export default client
