import apiClient from '@/api/client.js'

const state = {
  certificates: [],
  profiles: JSON.parse(localStorage.getItem('profiles')) || [],
  requests: []
}

const mutations = {
  setCertificates (state, certificates) {
    state.certificates = certificates
  },
  setRequests (state, requests) {
    state.requests = requests
  },
  addCertificate (state, result) {
    state.certificates.push(result.proof)
    state.profiles.push(result.profile)
    localStorage.setItem('profiles', JSON.stringify(state.profiles))
  }
}

const actions = {
  fetchRequests ({ commit }) {
    const requests = [
      { userName: '栗原萌実' },
      { userName: '鈴木太郎' },
      { userName: '山田一郎' }
    ]
    commit('setRequests', requests)
  },
  issueCertificates ({ commit }) {

  },
  fetchProofs ({ commit }) {
    apiClient.fetchProofs(state.profiles).then(respons => {
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
  requestIssue ({ commit }) {
    apiClient.requestIssue({ address: 'U9F1B20C8E6DD429A90C090BF39334934' }).then(respons => {
      console.log(respons)
      const result = {
        proof: {
          issuerName: '沖縄大学',
          issueDate: '',
          detail: '卒業証明',
          type: '取得',
          issuerSign: false,
          userSign: false
        },
        profile: {
          address: respons.profile.Address,
          issureAddress: 'U9F1B20C8E6DD429A90C090BF39334934',
          privateKey: respons.profile.PrivateKey,
          publickKey: respons.profile.PublickKey
        }
      }
      commit('addCertificate', result)
      return new Promise((resolve, reject) => {
        resolve()
      })
    })
  },
  publishCertificates ({ commit }, param) {
    const sharer = {
      Token: 'MzJiNmFlMmEtYWRlOS00ODA4LWJhNDctMTk2NjAxNDk3YWY2',
      PrivateKey: 'ii7+c9qCfoZoI1tHda/8AqNXl//FdAXFdcmUnmDJXIs='
    }

    apiClient.publishProofs(sharer).then(respons => {
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
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
