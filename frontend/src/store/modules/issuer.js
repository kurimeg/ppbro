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
  fetchIssuers ({ commit }) {
    apiClient.fetchIssuers().then(respons => {
      console.log(respons)
      const issures = respons.issures.map(item => {
        return {
          name: item.Name,
          src: item.Address,
          publicKey: item.Pubkey
        }
      })
      commit('setIssuers', issures)
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
