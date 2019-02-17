import apiClient from '@/api/client.js'

const state = {
  certificates: [],
  profiles: JSON.parse(localStorage.getItem('profiles')) || [],
  requestProfiles: []
}

const mutations = {
  setCertificates (state, certificates) {
    state.certificates = certificates
  },
  addCertificate (state, result) {
    state.certificates.push(result.proof)
    localStorage.setItem('requestProfiles', JSON.stringify(result.profile))
    state.profiles.push({ address: result.profile.Address })
    localStorage.setItem('profiles', JSON.stringify(state.profiles))
  },
  setSendResult (state, result) {
    localStorage.setItem('sendResult', JSON.stringify(result))
  }
}

const actions = {
  fetchProofs ({ commit }) {
    apiClient.fetchProofs(state.profiles).then(response => {
      console.log(response)
      const proofs = response.proofs.map(item => {
        return {
          issuerName: item.OrgName,
          issueDate: item.DateTime,
          detail: '',
          type: item.Value,
          issuerSign: item.Verified,
          userSign: ''
        }
      })
      commit('setCertificates', proofs)
    })
  },
  fetchRequests ({ commit }) {
    apiClient.fetchRequests().then(response => {
      console.log(response)
      const account = JSON.parse(localStorage.getItem('account'))
      const proofs = response.proofs.filter(item => item.OrgSign === '').map(item => {
        return {
          address: item.Address,
          issuerName: account.name + ' ' + account.nameEng,
          issueDate: '',
          detail: '卒業証明',
          type: '取得',
          issuerSign: item.MySign,
          userSign: ''
        }
      })
      commit('setCertificates', proofs)
    })
  },
  requestIssue ({ commit }, issuer) {
    apiClient.requestIssue({ address: issuer.src }).then(response => {
      console.log('requestIssue')
      console.log(response)
      const result = {
        proof: {
          issuerName: issuer.name,
          issueDate: '',
          detail: '卒業証明',
          type: '取得',
          issuerSign: false,
          userSign: false
        },
        profile: response.profile
      }
      commit('addCertificate', result)
      return new Promise((resolve, reject) => {
        resolve()
      })
    })
  },
  publishProofs ({ commit }, param) {
    apiClient.publishProofs(param).then(response => {
      console.log(response)
      const sendResult = {
        accessToken: response.send.AccessToken,
        destination: {
          address: response.send.Destination.Address,
          privateKey: response.send.Destination.PrivateKey,
          publicKey: response.send.Destination.PublicKey
        }
      }
      commit('setSendResult', sendResult)
    })
  },
  fetchPublishedProofs ({ commit }) {
    const sharer = {
      Token: 'MzJiNmFlMmEtYWRlOS00ODA4LWJhNDctMTk2NjAxNDk3YWY2',
      PrivateKey: 'ii7+c9qCfoZoI1tHda/8AqNXl//FdAXFdcmUnmDJXIs='
    }

    apiClient.fetchPublishedProofs(sharer).then(respons => {
      console.log(respons)
      const proofs = respons.proofs.map(item => {
        return {
          issuerName: item.OrgName,
          issueDate: item.DateTime,
          detail: '',
          type: item.Value,
          issuerSign: item.Verified,
          userSign: ''
        }
      })
      commit('setCertificates', proofs)
    })
  },

  issueProofs ({ commit }, param) {
    apiClient.issueProofs(param).then(response => {
      console.log(response)
      this.fetchRequests()
    })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
