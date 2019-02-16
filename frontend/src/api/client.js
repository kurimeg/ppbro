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
      client.get('/account/issuers')
        .then(res => resolve({ issures: res }))
        .catch(err => {
          reject(new Error(err.message))
        })
    })
  },

  fetchRequest: () => {
    return new Promise((resolve, reject) => {
      client.get('/api/account/requestedprofiles/U8F1B20C8E6DD429A90C090BF39334934')
        .then(res => resolve({ proofs: res }))
        .catch(err => {
          reject(new Error(err.message))
        })
    })
  },

  requestIssue: issuer => {
    return new Promise((resolve, reject) => {
      client.post('/account/profile', issuer)
        .then(res => resolve({ profile: res }))
        .catch(err => {
          reject(new Error(err.message))
        })
    })
  },

  publishProofs: param => {
    return new Promise((resolve, reject) => {
      client.post('/proof/send', {
        targetOrgAddress: 'U4F1B20C8E6DD429A90C090BF39334934',
        // targetOrgAddress: param.sharers[0].src,
        profileAddressList: ['d8a6f80a-a099-488c-afc0-c22dd63dd457', 'd8a6f80a-a099-488c-afc0-c22dd63dd457']
        // profileAddressList: param.certificateKeys
      })
        .then(res => resolve({ send: res }))
        .catch(err => {
          reject(new Error(err.message))
        })
    })
  }

}
