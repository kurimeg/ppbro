import client from './index'

export default {

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

  fetchRequests: () => {
    return new Promise((resolve, reject) => {
      client.get('/account/requestedprofiles/U8F1B20C8E6DD429A90C090BF39334934')
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
  },

  issueProofs: param => {
    return new Promise((resolve, reject) => {
      client.post('/proof', param.map(item => {
        return {
          profileAddress: item.address,
          value: '卒業証明　取得',
          privateKey: localStorage.getItem('issuerPrivateKey'),
          issuerAddress: 'U4F1B20C8E6DD429A90C090BF39334934'
        }
      }))
        .then(res => resolve({ send: res }))
        .catch(err => {
          reject(new Error(err.message))
        })
    })
  },

  fetchPublishedProofs: sharer => {
    return new Promise((resolve, reject) => {
      client.post('/proof/show', sharer)
        .then(res => resolve({ proofs: res }))
        .catch(err => {
          reject(new Error(err.message))
        })
    })
  }

}
