import apiClient from '@/api/client.js'

const state = {
  issuers: []
}

const mutations = {
  setIssuers (state, issuers) {
    state.issuers = issuers
  }
}

const actions = {
  fetchIssuers ({ commit }, searchWord) {
    apiClient.fetchIssuers().then(respons => {
      console.log(respons)
      const issures = respons.issures.map(item => {
        return {
          name: item.Name,
          src: item.Address,
          publicKey: item.Pubkey
        }
      })
      const filterdissures = issures.filter(issuer => issuer.name.startsWith(searchWord)
      )
      commit('setIssuers', filterdissures)
    })
  },

  clearIssuers ({ commit }) {
    commit('setIssuers', [])
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
