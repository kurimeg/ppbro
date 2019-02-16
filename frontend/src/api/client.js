import client from './index'

export default {
  login: authInfo => {
    return new Promise((resolve, reject) => {
      client.post('/auth/login', authInfo)
        .then(res => resolve({ token: res.data.token, userId: res.data.userId }))
        .catch(err => {
          reject(new Error(err.response.data.message || err.message))
        })
    })
  },

  logout: token => {
    return new Promise((resolve, reject) => {
      client.delete('/auth/logout', { headers: { 'x-kbn-token': token } })
        .then(() => resolve())
        .catch(err => {
          reject(new Error(err.response.data.message || err.message))
        })
    })
  },

  fetchProofs: profiles => {
    return new Promise((resolve, reject) => {
      const query = profiles.map(item => item.address).join('&address=')
      console.log('/proof?address=' + query)
      client.get('/proof/a?address=' + query)
        .then(res => resolve({ proofs: res }))
        .catch(err => {
          reject(new Error(err.message))
        })
    })
  },

  fetchIssuers: () => {
    return new Promise((resolve, reject) => {
      client.get('/iwa/issuers')
        .then(res => resolve({ issures: res }))
        .catch(err => {
          reject(new Error(err.message))
        })
    })
  }

}
