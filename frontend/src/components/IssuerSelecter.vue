<template>
  <v-dialog
      v-model="visible"
      transition="dialog-bottom-transition"
  >
    <v-btn slot="activator" flat icon color="accent">
      <v-icon large>add</v-icon>
    </v-btn>
    <v-card tile>
      <v-toolbar dark color="accent">
        <v-btn icon dark @click.native="visible = false">
          <v-icon>close</v-icon>
        </v-btn>
        <v-toolbar-title>発行者選択</v-toolbar-title>
      </v-toolbar>

      <v-form>
        <v-container>

          <v-layout row wrap>
            <v-flex xs12 md6 offset-md3>
              <v-text-field
                v-model="searchWord"
                solo
                clearable
                label="発行者名"
                append-icon="search"
                @change="fetchIssuers(searchWord)"
              ></v-text-field>
            </v-flex>
          </v-layout>

          <v-layout row wrap>
            <v-flex xs12 md6 offset-md3>
                <v-list>
                  <v-list-tile
                  v-for="(issuer, index) in issuers"
                  :key="index"
                  avatar
                  @click="selectIssuer"
                  >
                    <!-- <v-list-tile-avatar>
                      <img :src="!issuer.src ? require('@/assets/university.svg') : issuer.src">
                    </v-list-tile-avatar> -->

                    <v-list-tile-content>
                    <v-list-tile-title v-html="issuer.name"></v-list-tile-title>
                    </v-list-tile-content>

                    <!-- <v-list-tile-action>
                      <v-icon :color="item.active ? 'teal' : 'grey'">chat_bubble</v-icon>
                    </v-list-tile-action> -->
                  </v-list-tile>
                </v-list>
            </v-flex>
          </v-layout>

        </v-container>
      </v-form>
    </v-card>
  </v-dialog>
</template>

<script>
import { mapActions, mapState } from 'vuex'

export default {
  data: function () {
    return {
      visible: false,
      searchWord: ''
    }
  },
  computed: {
    ...mapState('issuer', ['issuers'])
  },
  watch: {
    visible: function (visible) {
      if (visible) this.initialize()
    }
  },
  methods: {
    ...mapActions({
      fetchIssuers: 'issuer/fetchIssuers',
      clearIssuers: 'issuer/clearIssuers',
      requestIssue: 'certificate/requestIssue'
    }),
    initialize: function () {
      this.searchWord = ''
      this.clearIssuers()
    },
    selectIssuer: function () {
      this.requestIssue()
        .then(() => {
        })
        .finally(() => {
          this.visible = false
        })
    }
  }
}
</script>

<style scoped>
.certificate{
  border: solid 1px #489DAB;
}
</style>
