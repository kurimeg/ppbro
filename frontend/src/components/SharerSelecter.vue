<template>
  <v-dialog
      v-model="visible"
      fullscreen
      hide-overlay
      transition="dialog-bottom-transition"
  >
    <v-btn
      fab
      dark
      small
      color="accent"
      slot="activator"
    >
      <v-icon>publish</v-icon>
    </v-btn>
    <v-card tile>
      <v-toolbar dark color="accent">
        <v-btn icon dark @click.native="visible = false">
          <v-icon>close</v-icon>
        </v-btn>
        <v-toolbar-title>公開</v-toolbar-title>
        <v-spacer></v-spacer>
        <v-toolbar-items>
          <v-btn dark flat @click="onPublish">公開<v-icon>publish</v-icon></v-btn>
        </v-toolbar-items>
      </v-toolbar>

      <v-container>
        <v-layout row wrap>
          <v-flex xs12 md4 pa-1>
            <v-text-field
              v-model="searchWord"
              solo
              clearable
              label="公開先"
              append-icon="search"
              @keydown.enter="fetchSharers"
            ></v-text-field>
              <v-list>
                <v-list-tile
                  v-for="sharer in sharers"
                  :key="sharer.name"
                >

                  <v-list-tile-action>
                    <v-checkbox v-model="selectedSharers" :value="sharer"></v-checkbox>
                  </v-list-tile-action>

                  <v-list-tile-content>
                  <v-list-tile-title v-html="sharer.name"></v-list-tile-title>
                  </v-list-tile-content>

                </v-list-tile>
              </v-list>
          </v-flex>

          <v-flex xs12 md7 offset-md1 pa-1>
            <h3 class="certificate-text">公開するスキルを選択してください</h3>
            <v-container fluid grid-list-sm>
              <v-layout row wrap>
                  <timeline-certificate
                    v-for="certificate in certificates"
                    :key="certificate.issuerName"
                    :certificate="certificate"
                    @clicked="onCertificateClicked"
                  />
              </v-layout>
            </v-container>
          </v-flex>
        </v-layout>

      </v-container>
    </v-card>
  </v-dialog>
</template>

<script>
import TimelineCertificate from '@/components/TimelineCertificateEditor'
import { mapActions, mapState } from 'vuex'

export default {
  components: {
    TimelineCertificate
  },
  data: function () {
    return {
      visible: false,
      searchWord: '',
      selectedCertificateKeys: [],
      selectedSharers: []
    }
  },
  computed: {
    ...mapState('sharer', ['sharers']),
    ...mapState('certificate', ['certificates'])
  },
  watch: {
    visible: function (visible) {
      if (visible) this.initialize()
    }
  },
  methods: {
    ...mapActions({
      fetchSharers: 'sharer/fetchSharers',
      clearSharers: 'sharer/clearSharers',
      publishCertificates: 'certificate/publishProofs'
    }),
    initialize: function () {
      this.searchWord = ''
      this.clearSharers()
    },
    onPublish: function () {
      const param = {
        certificateKeys : this.selectedCertificateKeys,
        sharers: this.selectedSharers
      }
      this.publishCertificates(param)
        .then(() => {

        })
        .finally(() => {
          this.visible = false
        })
    },
    onCertificateClicked: function (certificate) {
      if (certificate.selected) {
        this.selectedCertificateKeys.push(certificate.key)
      } else {
        const index = this.selectedCertificateKeys.indexOf(certificate.key)
        this.selectedCertificateKeys.splice(index, 1);
      }    
    }
  }
}
</script>

<style scoped>
.certificate{
  border: solid 1px #489DAB;
}
.certificate-add{
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
  height: 20vh;
  border: dashed 1px #489DAB;
}
.certificate-text{
  color: #707070;
}
</style>